using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Trouble_Ticket_Manager.Models.Entities
{
    public class TicketComputer
    {
        public int Id { get; set; }
        [Required]
        public string IssueDescription { get; set; } = string.Empty;
        public int TicketId { get; set; }
        public string ComputerAssetTag { get; set; } = string.Empty;
        [JsonIgnore]
        public Ticket? Ticket { get; set; }
        [JsonIgnore]
        public Computer? Computer { get; set; }
    }
}