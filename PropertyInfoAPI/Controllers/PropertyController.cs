using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PropertyInfoAPI.Models;
using PropertyInfoAPI.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PropertyInfoAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PropertyController : ControllerBase
    {
        private readonly IPropertyRepository _propertyRepository;

        public PropertyController(IPropertyRepository propertyRepository)
        {
            this._propertyRepository = propertyRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllProperties()
        {
            var records = await _propertyRepository.getAllPropertiesAsync();

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
        public async Task<IActionResult> GetPropertyById([FromRoute] int id)
        {
            var feature = await _propertyRepository.GetPropertyByIdAsync(id);
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
        public async Task<IActionResult> AddNewPropertyEngine([FromBody] PropertyModel propertyModel)
        {
            var id = await _propertyRepository.AddPropertyAsync(propertyModel);
            if (id != -1)
            {
                return CreatedAtAction(nameof(GetPropertyById), new { id = id, Controller = "property" }, id);
            }
            else
            {
                return StatusCode(500, "Can not add data at this moment");
            }

        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateProperty([FromBody] PropertyModel propertyModel, [FromRoute] int id)
        {
            var status = await _propertyRepository.UpdatePropertyAsync(id, propertyModel);
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
        public async Task<IActionResult> DeleteProperty([FromRoute] int id)
        {
            var status = await _propertyRepository.DeletePropertyAsync(id);
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
