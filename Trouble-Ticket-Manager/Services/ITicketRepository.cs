using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Trouble_Ticket_Manager.Models.DTOs;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public interface ITicketRepository
    {
        Task<ICollection<Ticket>> ReadAllAsync();
        Task<Ticket?> ReadAsync(int id);
        Task<Ticket> CreateAsync(Ticket newTicket);
        Task UpdateAsync(int id, Ticket ticket);
        Task DeleteAsync(int id);
        Task<ICollection<TicketDTO>> ReadAllDtoAsync();
    }
}