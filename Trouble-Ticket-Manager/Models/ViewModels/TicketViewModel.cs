using System.ComponentModel.DataAnnotations;
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
    [StringLength(6, ErrorMessage = "Asset Tag must be 6 alphanumeric characters")]
    public string? NewAssetTag { get; set; }
    [StringLength(7, ErrorMessage = "Service Tag must be 6 alphanumeric characters")]
    public string? NewComputerServiceTag { get; set; }
    public string? NewComputerModel { get; set; }

    // --- NEW CONTACT CREATION FIELDS ---
    public string? NewContactName { get; set; }
    [EmailAddress(ErrorMessage = "Invalid Email Address format.")]
    public string? NewContactEmail { get; set; }
    public string? NewContactBuilding { get; set; }
    public string? NewContactRoom { get; set; }
}