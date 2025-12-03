using System.ComponentModel.DataAnnotations;

namespace Trouble_Ticket_Manager.Models.Entities
{
    public class Ticket
    {
        public int Id { get; set; }
        public int ContactId { get; set; }

        [Required]
        public User Contact { get; set; } = null!;

        public DateTime SubmittedAt { get; set; }
        public bool? IsResolved { get; set; }
        public bool? ChargerGiven { get; set; }
        public ICollection<TicketComputer> TicketComputers { get; set; } 
            = new List<TicketComputer>();
        
    }
}