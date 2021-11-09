using ContactAPI.Models;
using ContactAPI.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContactAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ContactController : ControllerBase
    {
        private readonly IContactRepository _contactRepository;

        public ContactController(IContactRepository contactRepository)
        {
            this._contactRepository = contactRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> GetAllContact()
        {
            var contacts = await _contactRepository.getAllContactAsync();
            return Ok(contacts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetContactById([FromRoute] int id)
        {
            var contact = await _contactRepository.GetContactByIdAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            else
            {
                return Ok(contact);
            }
        }

        [HttpPost("")]
        public async Task<IActionResult> AddNewContact([FromBody] ContactModel contactModel)
        {
            var id = await _contactRepository.AddContactAsync(contactModel);
            if (id != -1)
            {
                return CreatedAtAction(nameof(GetContactById), new { id = id, Controller = "contact" }, id);
            }
            else
            {
                return StatusCode(500, "Can not add data at this moment");
            }
            
  
            
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateContact([FromBody] ContactModel contactModel, [FromRoute] int id)
        {
            var status= await _contactRepository.UpdateContactAsync(id, contactModel);
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
        public async Task<IActionResult> DeleteContact([FromRoute] int id)
        {
            var status=await _contactRepository.DeleteContactAsync(id);
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
