using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Trouble_Ticket_Manager.Models.DTOs
{
    public class ComputerDTO
    {
        public int Id { get; set; }
        public string AssetTag { get; set; } = string.Empty;
        public string ServiceTag { get; set; } = string.Empty;
        public string Model { get; set; } = string.Empty;
        public bool UnderWarranty { get; set; }
    }
}