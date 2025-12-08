using Microsoft.AspNetCore.Mvc;
using Trouble_Ticket_Manager.Models.ViewModels;
using Trouble_Ticket_Manager.Services;
using CsvHelper; 
using System.Globalization;
using Trouble_Ticket_Manager.Models.DTOs;
using Trouble_Ticket_Manager.Models.Entities;
using Microsoft.AspNetCore.Authorization;

namespace Trouble_Ticket_Manager.Controllers;

[Authorize]
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
        var model = new TicketViewModel();
        return View(model);
    }

    [HttpGet]
    public async Task<IActionResult> DetailsPartial(int id)
    {
        var ticket = await _ticketRepo.ReadAsync(id);

        if (ticket == null)
        {
            return NotFound("Ticket not found.");
        }

        return PartialView("_DetailsModal", ticket);
    }

    [HttpGet("Ticket/Edit/{id}")]
    public async Task<IActionResult> Edit(int id)
    {
        var ticketEntity = await _ticketRepo.ReadAsync(id);
        if (ticketEntity == null)
        {
            return NotFound();
        }

        var currentAssetTags = ticketEntity.TicketComputers
            .Select(tc => tc.ComputerAssetTag)
            .ToList();

        var issueDescription = ticketEntity.TicketComputers.FirstOrDefault()?.IssueDescription ?? string.Empty;

        var model = new TicketViewModel
        {
            Id = ticketEntity.Id,

            SubmittedAt = ticketEntity.SubmittedAt,

            IssueDescription = issueDescription,

            IsResolved = ticketEntity.IsResolved ?? false,
            ChargerGiven = ticketEntity.ChargerGiven ?? false,

            SelectedContactId = ticketEntity.ContactId,

            SelectedAssetTags = currentAssetTags,

        };


        return View(model);
    }

    [HttpGet("Ticket/Close/{id}")]
    public async Task<IActionResult> CloseConfirmation(int id)
    {
        var ticket = await _ticketRepo.ReadAsync(id);
        if (ticket == null)
        {
            TempData["ErrorMessage"] = $"Error: Ticket ID {id} was not found.";
            return RedirectToAction(nameof(Index));
        }

        return View(ticket);
    }


    public async Task<IActionResult> ExportAll()
    {
        var allTickets = await _ticketRepo.ReadAllWithDetailsAsync();

        var records = allTickets.Select(t =>
        {
            var ticketComputers = t.TicketComputers ?? new List<TicketComputer>();

            return new TicketExportDto
            {
                Id = t.Id,

                SubmittedAtDisplay = t.SubmittedAt.Year > 1 ? t.SubmittedAt.ToString("yyyy-MM-dd HH:mm") : "N/A",

                ContactName = t.Contact?.Name ?? "N/A",
                ContactEmail = t.Contact?.Email ?? "N/A",

                IssueDescription = ticketComputers.FirstOrDefault()?.IssueDescription ?? "N/A",

                ComputerAssetTags = string.Join(", ",
                    ticketComputers.Select(tc => tc.ComputerAssetTag)),

                ComputerModels = string.Join(", ",
                    ticketComputers.Select(tc => tc.Computer?.Model ?? "Unknown Model")),

                IsResolved = t.IsResolved.HasValue ? (t.IsResolved.Value ? "Yes" : "No") : "Pending",
                ChargerGiven = t.ChargerGiven.HasValue ? (t.ChargerGiven.Value ? "Yes" : "No") : "N/A"

            };
        }).ToList();

        byte[] fileBytes;

        using (var memoryStream = new MemoryStream())
        using (var writer = new StreamWriter(memoryStream))
        using (var csv = new CsvWriter(writer, CultureInfo.InvariantCulture))
        {
            csv.WriteRecords(records);
            writer.Flush();

            fileBytes = memoryStream.ToArray();
        }

        return File(
            fileContents: fileBytes,
            contentType: "text/csv",
            fileDownloadName: $"Ticket_Report_{DateTime.Now:yyyyMMdd_HHmmss}.csv"
        );
    }

}
