using System.ComponentModel.DataAnnotations;

namespace Trouble_Ticket_Manager.Models.Entities
{
    public class Computer
    {
        public int Id { get; set; }
        [StringLength(6)]
        public string AssetTag { get; set; } = string.Empty;
        [StringLength(7)]
        public string ServiceTag { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public int? UserId { get; set; }
        public User? User { get; set; }
        public ICollection<TicketComputer> TicketComputers { get; set; } 
            = new List<TicketComputer>();
    }
}