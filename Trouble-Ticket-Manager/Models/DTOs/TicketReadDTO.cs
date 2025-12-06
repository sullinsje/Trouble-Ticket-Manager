using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Trouble_Ticket_Manager.Models.DTOs
{
    public class TicketReadDTO
    {
        public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public string AssetTags { get; set; } = string.Empty;
        public bool? IsResolved { get; set; }
        public bool? ChargerGiven { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public ICollection<TicketComputerDTO> TicketComputers { get; set; }
            = new List<TicketComputerDTO>();

    }

}