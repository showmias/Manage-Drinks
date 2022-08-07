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
    public class BeersController : ControllerBase
    {
        private readonly IBeerRepository _beerRepo;
        private readonly ILogger<BeersController> _logger;

        public BeersController(ILogger<BeersController> logger, IBeerRepository beerRepo)
        {
            _logger = logger;
            _beerRepo = beerRepo;
        }

        /// <summary>
        /// GET: api/Beers
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeers()
        {
            var beers = await _beerRepo.GetAll();
            return Ok(beers);
        }

        /// <summary>
        /// GET: api/Beers/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Beer>> GetBeer(int id)
        {
            var beer = await _beerRepo.GetById(id);

            if (beer == null)
            {
                return NotFound();
            }

            return Ok(beer);
        }

        /// <summary>
        /// To get Beer based on Volume
        /// </summary>
        /// <param name="gtAlcoholByVolume"></param>
        /// <param name="ltAlcoholByVolume"></param>
        /// <returns></returns>
        [HttpGet("{gtAlcoholByVolume?}/{ltAlcoholByVolume?}")]
        public async Task<ActionResult<IEnumerable<Beer>>> GetBeer(decimal gtAlcoholByVolume = 0, decimal ltAlcoholByVolume = 0)
        {
            IEnumerable<Beer> beers;
            if (gtAlcoholByVolume != 0 && ltAlcoholByVolume != 0)
                beers = await _beerRepo.GetBeerByVolume(gtAlcoholByVolume, ltAlcoholByVolume);
            else
                beers = await _beerRepo.GetAll();

            if (beers == null)
            {
                return NotFound();
            }

            return Ok(beers);
        }

        /// <summary>
        /// PUT: api/Beers/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="beer"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBeer(int id, Beer beer)
        {
            if (id != beer.BeerId)
            {
                return BadRequest();
            }

            try
            {
                await _beerRepo.UpdateRecord(beer);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BeerExists(id))
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
        /// POST: api/Beers
        /// </summary>
        /// <param name="beer"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Beer>> PostBeer(Beer beer)
        {
            await _beerRepo.InsertRecord(beer);

            return CreatedAtAction("GetBeer", new { id = beer.BeerId }, beer);
        }

        /// <summary>
        /// To check record esists by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BeerExists(int id)
        {
            return _beerRepo.IsRecordExists(id);
        }
    }
}
