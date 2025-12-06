using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Models;
using Trouble_Ticket_Manager.Models.DTOs;
using Trouble_Ticket_Manager.Models.ViewModels;
using Trouble_Ticket_Manager.Services;

namespace Trouble_Ticket_Manager.Controllers;

public class TicketController : Controller
{
    private readonly ITicketRepository _ticketRepo;
    public TicketController(ITicketRepository ticketRepo)
    {
        _ticketRepo = ticketRepo;
    }
    public async Task<IActionResult> Index()
    {
        return View();
    }

    public IActionResult Create()
    {
        var model = new TicketVM();
        return View(model);
    }

    public async Task<IActionResult> Details(int id)
    {
        var ticket = await _ticketRepo.ReadAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return View(ticket);
    }

    [HttpGet("Ticket/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var ticket = await _ticketRepo.ReadAsync(id);
        if (ticket == null)
        {
            return NotFound();
        }
        return View(ticket);
    }

    public async Task<IActionResult> Delete(int id)
    {
        return View();
    }

}
