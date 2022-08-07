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
    public class BarsController : ControllerBase
    {
        private readonly IBarRepository _barRepo;
        private readonly ILogger<BarsController> _logger;

        public BarsController(ILogger<BarsController> logger, IBarRepository barRepo)
        {
            _logger = logger;
            _barRepo = barRepo;
        }

        /// <summary>
        /// GET: api/Bars
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Bar>>> GetBars()
        {
            var bars = await _barRepo.GetAll();
            return Ok(bars);
        }

        /// <summary>
        /// GET: api/Bars/5
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<Bar>> GetBar(int id)
        {
            if (id == 0)
            {
                return BadRequest();
            }
            var bar = await _barRepo.GetById(id);

            if (bar == null)
            {
                return NotFound();
            }

            return bar;
        }

        /// <summary>
        /// PUT: api/Bars/5
        /// </summary>
        /// <param name="id"></param>
        /// <param name="bar"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> PutBar(int id, Bar bar)
        {
            if (id != bar.BarId)
            {
                return BadRequest();
            }

            try
            {
                await _barRepo.UpdateRecord(bar);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!BarExists(id))
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
        /// POST: api/Bars
        /// </summary>
        /// <param name="bar"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<ActionResult<Bar>> PostBar(Bar bar)
        {
            await _barRepo.InsertRecord(bar);

            return CreatedAtAction("GetBar", new { id = bar.BarId }, bar);
        }

        /// <summary>
        /// To check record exists
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        private bool BarExists(int id)
        {
            return _barRepo.IsRecordExists(id);
        }
    }
}
