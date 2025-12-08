using Microsoft.AspNetCore.Mvc.Rendering;

namespace Trouble_Ticket_Manager.Models.ViewModels;

public class TicketViewModel
{
    // --- TICKET CORE ---
    public int Id {get; set;}
    public DateTime SubmittedAt { get; set; }
    public string IssueDescription { get; set; } = string.Empty;
    public bool IsResolved { get; set; }
    public bool ChargerGiven { get; set; }

    // --- CONTACT FIELDS (USED FOR SELECTION) ---
    public int? SelectedContactId { get; set; }
    public string ContactName { get; set; } = string.Empty; 
    public IEnumerable<SelectListItem>? ContactOptions { get; set; }
    public List<string>? SelectedAssetTags { get; set; } 
    
    // --- NEW COMPUTER CREATION FIELDS ---
    public string? NewAssetTag { get; set; }
    public string? NewComputerServiceTag { get; set; }
    public string? NewComputerModel { get; set; }

    // --- NEW CONTACT CREATION FIELDS ---
    public string? NewContactName { get; set; }
    public string? NewContactEmail { get; set; }
    public string? NewContactBuilding { get; set; }
    public string? NewContactRoom { get; set; }
}