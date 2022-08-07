using ManageDrinks.Dtos;
using ManageDrinks.Models;
using ManageDrinks.Repositories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ManageDrinks.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BreweryBeersController : ControllerBase
    {
        private readonly IBreweryBeersRepository _breweryBeersRepo;
        private readonly ILogger<BreweryBeersController> _logger;

        public BreweryBeersController(ILogger<BreweryBeersController> logger, IBreweryBeersRepository breweryBeersRepo)
        {
            _logger = logger;
            _breweryBeersRepo = breweryBeersRepo;
        }

        /// <summary>
        /// GET: api/Breweries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BreweryBeersDto>>> GetAllBreweryBeers()
        {
            var breweryBeers = await _breweryBeersRepo.GetAll();

            var result = breweryBeers.GroupBy(x => x.BreweryId).Select(y => new BreweryBeersDto { BreweryId = y.Key, Beers = y.ToList().Select(x => x.Beer).ToList() });

            return Ok(result);
        }

        /// <summary>
        /// GET: api/Breweries/5
        /// </summary>
        /// <param name="breweryId"></param>
        /// <returns></returns>
        [HttpGet("{breweryId}")]
        public async Task<ActionResult<BreweryBeersDto>> GetBreweryBeers(int breweryId)
        {
            var breweryBeers = await _breweryBeersRepo.GetBreweryBeersById(breweryId);

            if (breweryBeers.Count() == 0)
            {
                return NotFound();
            }
            var result = breweryBeers.GroupBy(x => x.BreweryId).Select(y => new BreweryBeersDto { BreweryId = y.Key, Beers = y.ToList().Select(x => x.Beer).ToList() });

            return Ok(result);
        }

        /// <summary>
        /// To insert new record
        /// </summary>
        /// <param name="breweryBeers"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BreweryBeers>> PostBreweryBeers(BreweryBeers breweryBeers)
        {
            await _breweryBeersRepo.InsertRecord(breweryBeers);
            return CreatedAtAction("GetAllBreweryBeers", new { id = breweryBeers.BreweryBeerId }, breweryBeers);
        }
    }
}
