using FeaturesAPI.Models;
using FeaturesAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FeaturesAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FeaturesController : ControllerBase
    {
        private readonly IFeaturesRepository _featuresRepository;

        public FeaturesController(IFeaturesRepository featuresRepository)
        {
            this._featuresRepository = featuresRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllContact()
        {
            var contacts = await _featuresRepository.getAllFeaturesAsync();

            if (contacts != null)
            {
                return Ok(contacts);
            }
            else
            {
                return StatusCode(500, "Data is not available!");
            }
            
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetFeatureById([FromRoute] int id)
        {
            var feature = await _featuresRepository.GetFeatureByIdAsync(id);
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
        public async Task<IActionResult> AddNewFeature([FromBody] FeatureModel featureModel)
        {
            var id = await _featuresRepository.AddFeatureAsync(featureModel);
            if (id != -1)
            {
                return CreatedAtAction(nameof(GetFeatureById), new { id = id, Controller = "features" }, id);
            }
            else
            {
                return StatusCode(500, "Can not add data at this moment");
            } 
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateFeature([FromBody] FeatureModel featureModel, [FromRoute] int id)
        {
            var status = await _featuresRepository.UpdateFeatureAsync(id, featureModel);
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
        public async Task<IActionResult> DeleteFeature([FromRoute] int id)
        {
            var status = await _featuresRepository.DeleteFeatureAsync(id);
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
