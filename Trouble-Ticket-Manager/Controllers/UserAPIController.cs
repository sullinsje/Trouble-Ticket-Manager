using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Models.Entities;
using Trouble_Ticket_Manager.Services;

namespace Trouble_Ticket_Manager.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserAPIController : ControllerBase
    {
        private readonly IUserRepository _userRepo;

        public UserAPIController(IUserRepository userRepo)
        {
            _userRepo = userRepo;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create(User newUser)
        {
            if (ModelState.IsValid)
            {
                await _userRepo.CreateAsync(newUser);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet("all")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userRepo.ReadAllAsync();
            return Ok(users);
        }

        [HttpGet("one/{id}")]
        public async Task<IActionResult> GetOne(int id)
        {
            var user = await _userRepo.ReadAsync(id);
            if (user == null)
            {
                return NotFound();
            }
            return Ok(user);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update([FromForm] User user)
        {
            await _userRepo.UpdateAsync(user.Id, user);
            return NoContent(); // 204 as per HTTP specification
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _userRepo.DeleteAsync(id);
            return NoContent(); // 204 as per HTTP specification
        }
    }
}