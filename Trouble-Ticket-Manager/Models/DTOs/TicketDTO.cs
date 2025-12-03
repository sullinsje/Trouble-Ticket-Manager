namespace Trouble_Ticket_Manager.Models.DTOs
{
    public class TicketDTO
    {
        public int Id { get; set; }
        public DateTime SubmittedAt { get; set; }
        public bool? IsResolved { get; set; }
        public string ContactName { get; set; } = string.Empty;

        public ICollection<TicketComputerDTO> TicketComputers { get; set; }
            = new List<TicketComputerDTO>();
    }
}