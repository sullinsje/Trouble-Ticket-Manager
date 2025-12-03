using System.ComponentModel.DataAnnotations;

namespace Trouble_Ticket_Manager.Models.Entities
{
    public class TicketComputer
    {
        public int Id { get; set; }
        [Required]
        public string IssueDescription { get; set; } = string.Empty;
        public int TicketId { get; set; }
        public int ComputerId { get; set; }
        public Ticket? Ticket { get; set; }
        public Computer? Computer { get; set; }
    }
}