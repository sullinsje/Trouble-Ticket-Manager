using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Models.Entities;
using Trouble_Ticket_Manager.Services;

namespace Trouble_Ticket_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ContactAPIController : ControllerBase
    {
        private readonly IContactRepository _contactRepo;

        public ContactAPIController(IContactRepository contactRepo)
        {
            _contactRepo = contactRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(Contact newContact)
        {
            if (ModelState.IsValid)
            {
                await _contactRepo.CreateAsync(newContact);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var contacts = await _contactRepo.ReadAllAsync();
            return Ok(contacts);
        }

        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var contact = await _contactRepo.ReadAsync(id);
            if (contact == null)
            {
                return NotFound();
            }
            return Ok(contact);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] Contact contact)
        {
            await _contactRepo.UpdateAsync(contact.Id, contact);
            return NoContent(); // 204 as per HTTP specification
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _contactRepo.DeleteAsync(id);
            return NoContent(); // 204 as per HTTP specification
        }
    }
}