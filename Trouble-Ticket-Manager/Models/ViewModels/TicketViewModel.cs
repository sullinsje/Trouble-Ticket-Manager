using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trouble_Ticket_Manager.Models.ViewModels;
public class TicketVM
{
    public string ContactName { get; set; } = string.Empty;
    public IEnumerable<SelectListItem>? ContactOptions { get; set; }
    public string AssetTag { get; set; } = string.Empty;
    public IEnumerable<SelectListItem>? ComputerOptions { get; set; }
    public DateTime SubmittedAt { get; set; }
    public bool IsResolved { get; set; }
    public bool ChargerGiven { get; set; }
}