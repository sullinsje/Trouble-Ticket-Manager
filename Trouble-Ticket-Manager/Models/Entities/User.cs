using System.Text.Json.Serialization;

namespace Trouble_Ticket_Manager.Models.Entities
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string? Building { get; set; }
        public string? Room { get; set; }
        [JsonIgnore]
        public ICollection<Computer> Computers { get; set; } = new List<Computer>();
    }
}