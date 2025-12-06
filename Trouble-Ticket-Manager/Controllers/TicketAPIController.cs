using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Services;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketAPIController : ControllerBase
    {
        private readonly ITicketRepository _ticketRepo;

        public TicketAPIController(ITicketRepository ticketRepo)
        {
            _ticketRepo = ticketRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] Ticket newTicket)
        {
            if (ModelState.IsValid)
            {
                await _ticketRepo.CreateAsync(newTicket);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var tickets = await _ticketRepo.ReadAllDtoAsync();
            return Ok(tickets);
        }

        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var ticket = await _ticketRepo.ReadDtoAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] Ticket ticket)
        {
            await _ticketRepo.UpdateAsync(ticket.Id, ticket);
            return NoContent(); // 204 as per HTTP specification
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketRepo.DeleteAsync(id);
            return NoContent(); // 204 as per HTTP specification
        }
    }
}