using System.ComponentModel.DataAnnotations;

namespace Trouble_Ticket_Manager.Models.Entities
{
    public class Computer
    {
        [Key]
        [StringLength(6)]
        public string AssetTag { get; set; } = string.Empty;
        [StringLength(7)]
        public string ServiceTag { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool UnderWarranty { get; set; }
        public int? ContactId { get; set; }
        public Contact? Contact { get; set; }
        public ICollection<TicketComputer> TicketComputers { get; set; }
            = new List<TicketComputer>();
    }
}