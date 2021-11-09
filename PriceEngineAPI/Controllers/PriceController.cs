using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriceEngineAPI.Models;
using PriceEngineAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PriceEngineAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PriceController : ControllerBase
    {
        private readonly IPriceRepository _priceRepository;

        public PriceController(IPriceRepository priceRepository)
        {
            this._priceRepository = priceRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllPrices()
        {
            var records = await _priceRepository.getAllPricesAsync();

            if (records != null)
            {
                return Ok(records);
            }
            else
            {
                return StatusCode(500, "Data is not available!");
            }

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPriceById([FromRoute] int id)
        {
            var feature = await _priceRepository.GetPriceByIdAsync(id);
            if (feature == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(feature);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewPriceEngine([FromBody] PriceModel priceModel)
        {
            var id = await _priceRepository.AddPriceAsync(priceModel);
            if (id != -1)
            {
                return CreatedAtAction(nameof(GetPriceById), new { id = id, Controller = "price" }, id);
            }
            else
            {
                return StatusCode(500, "Can not add data at this moment");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdatePrice([FromBody] PriceModel priceModel, [FromRoute] int id)
        {
            var status = await _priceRepository.UpdatePriceAsync(id, priceModel);
            if (status)
            {
                return Ok("Data is successfully updated");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePrice([FromRoute] int id)
        {
            var status = await _priceRepository.DeletePriceAsync(id);
            if (status)
            {
                return Ok("Data is successfully deleted");
            }
            else
            {
                return StatusCode(500, "Requested data in not in the database");
            }
        }
    }
}
