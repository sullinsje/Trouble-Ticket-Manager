using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Models;
using Trouble_Ticket_Manager.Services;

namespace Trouble_Ticket_Manager.Controllers;

public class ComputerController : Controller
{
    private readonly IComputerRepository _computerRepo;
    public ComputerController(IComputerRepository computerRepo)
    {
        _computerRepo = computerRepo;
    }
    public async Task<IActionResult> Index()
    {
        var computers = await _computerRepo.ReadAllAsync();
        return View(computers);
    }

    public IActionResult Create()
    {
        return View();
    }

    public async Task<IActionResult> Details(string assetTag)
    {
        var computer = await _computerRepo.ReadAsync(assetTag);
        if (computer == null)
        {
            return NotFound();
        }
        return View(computer);
    }

    [HttpGet("Computer/Edit/{assetTag}")]
    public async Task<IActionResult> Edit(string assetTag)
    {
        var computer = await _computerRepo.ReadAsync(assetTag);
        if (computer == null)
        {
            return NotFound();
        }
        return View(computer);
    }

    public async Task<IActionResult> Delete(string assetTag)
    {
        return View();
    }

}
