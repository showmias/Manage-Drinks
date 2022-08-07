using ManageDrinks.Models;
using ManageDrinks.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ManageDrinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweriesController : ControllerBase
    {
        private readonly IBreweryRepository _breweryRepo;
        private readonly ILogger<BreweriesController> _logger;

        public BreweriesController(ILogger<BreweriesController> logger, IBreweryRepository breweryRepo)
        {
            _logger = logger;
            _breweryRepo = breweryRepo;
        }

        /// <summary>
        ///  GET: api/Breweries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Brewery>>> GetBreweries()
        {
            var breweries = await _breweryRepo.GetAll();
            return Ok(breweries);
        }

        /// <summary>
        /// GET: api/Breweries/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Brewery>> GetBrewery(int id)
        {
            var brewery = await _breweryRepo.GetById(id);

            if (brewery == null)
            {
                return NotFound();
            }

            return brewery;
        }

        /// <summary>
        /// PUT: api/Breweries/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="brewery"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBrewery(int id, Brewery brewery)
        {
            if (id != brewery.BreweryId)
            {
                return BadRequest();
            }

            try
            {
                await _breweryRepo.UpdateRecord(brewery);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BreweryExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        /// <summary>
        /// POST: api/Breweries
        /// </summary>
        /// <param name="brewery"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Brewery>> PostBrewery(Brewery brewery)
        {
            await _breweryRepo.InsertRecord(brewery);

            return CreatedAtAction("GetBrewery", new { id = brewery.BreweryId }, brewery);
        }

        /// <summary>
        /// To check record exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BreweryExists(int id)
        {
            return  _breweryRepo.IsRecordExists(id);
        }
    }
}
