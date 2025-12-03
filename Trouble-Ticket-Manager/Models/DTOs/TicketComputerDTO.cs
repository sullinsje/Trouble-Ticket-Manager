using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trouble_Ticket_Manager.Models.DTOs
{
    public class TicketComputerDTO
    {
        public int Id { get; set; }
        public string IssueDescription { get; set; } = string.Empty;
        public ComputerDTO Computer { get; set; } = null!;
    }
}