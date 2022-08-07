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
    public class BarBeersController : ControllerBase
    {
        private readonly IBarBeersRepository _barBeersRepo;
        private readonly ILogger<BarBeersController> _logger;

        public BarBeersController(ILogger<BarBeersController> logger, IBarBeersRepository barBeersRepo)
        {
            _logger = logger;
            _barBeersRepo = barBeersRepo;
        }

        /// <summary>
        /// GET: api/Breweries
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<BarBeersDto>>> GetAllBarBeers()
        {
            var barBeers = await _barBeersRepo.GetAll();
            var result = barBeers.GroupBy(x => x.BarId).Select(y => new BarBeersDto { BarId = y.Key, Beers = y.ToList().Select(x => x.Beer).ToList() });

            return Ok(result);
        }

        /// <summary>
        /// GET: api/Breweries/5
        /// </summary>
        /// <param name="barId"></param>
        /// <returns></returns>
        [HttpGet("{barId}")]
        public async Task<ActionResult<IEnumerable<BarBeersDto>>> GetBarBeers(int barId)
        {
            if (barId == 0)
            {
                return BadRequest();
            }
            var barBeers = await _barBeersRepo.GetBarBeersById(barId);
            if (barBeers == null || barBeers.Count() == 0)
            {
                return NotFound();
            }
            var result = barBeers.GroupBy(x => x.BarId).Select(y => new BarBeersDto { BarId = y.Key, Beers = y.ToList().Select(x => x.Beer).ToList() });

            return Ok(result);
        }

        /// <summary>
        /// To link Bar with Beers
        /// </summary>
        /// <param name="barBeers"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<BarBeers>> PostBarBeers(BarBeers barBeers)
        {
            await _barBeersRepo.InsertRecord(barBeers);

            return CreatedAtAction("GetAllBarBeers", new { id = barBeers.BarBeerId }, barBeers);
        }
    }
}
