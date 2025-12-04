using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Models.Entities;
using Trouble_Ticket_Manager.Services;

namespace Trouble_Ticket_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ComputerAPIController : ControllerBase
    {
        private readonly IComputerRepository _computerRepo;

        public ComputerAPIController(IComputerRepository computerRepo)
        {
            _computerRepo = computerRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromForm] Computer newComputer)
        {
            if (ModelState.IsValid)
            {
                await _computerRepo.CreateAsync(newComputer);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var computers = await _computerRepo.ReadAllAsync();
            return Ok(computers);
        }

        [HttpGet("one/{assetTag}")]
        public async Task<IActionResult> GetOne(string assetTag)
        {
            var computer = await _computerRepo.ReadAsync(assetTag);
            if (computer == null)
            {
                return NotFound();
            }
            return Ok(computer);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] Computer computer)
        {
            await _computerRepo.UpdateAsync(computer.AssetTag, computer);
            return NoContent(); // 204 as per HTTP specification
        }

        [HttpDelete("delete/{assetTag}")]
        public async Task<IActionResult> Delete(string assetTag)
        {
            await _computerRepo.DeleteAsync(assetTag);
            return NoContent(); // 204 as per HTTP specification
        }
    }
}