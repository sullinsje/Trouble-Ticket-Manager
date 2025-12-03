using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        [HttpPost]
        public async Task<IActionResult> Create(Computer newComputer)
        {
            if (ModelState.IsValid)
            {
                await _computerRepo.CreateAsync(newComputer);
                return Ok();
            }
            return NotFound();
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var computers = await _computerRepo.ReadAllAsync();
            return Ok(computers);
        }
    }
}