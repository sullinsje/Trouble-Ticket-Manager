using System.Collections;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Trouble_Ticket_Manager.Models.DTOs
{
    public class TicketUpdateDTO
    {
        public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool? IsResolved { get; set; }
        public string ContactName { get; set; } = string.Empty;
        public bool? ChargerGiven { get; set; }

    }

}