using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;
using Trouble_Ticket_Manager.Models.DTOs;

namespace Trouble_Ticket_Manager.Services
{
    public class DbTicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        public DbTicketRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Ticket> CreateAsync(Ticket newTicket)
        {
            await _db.Tickets.AddAsync(newTicket);
            await _db.SaveChangesAsync();
            return newTicket;
        }

        public async Task DeleteAsync(int id)
        {
            var ticketToDelete = await ReadAsync(id);
            if (ticketToDelete != null)
            {
                _db.Tickets.Remove(ticketToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Ticket>> ReadAllAsync()
        {
            return await _db.Tickets
            .Include(t => t.Contact)
            .Include(t => t.TicketComputers)
                .ThenInclude(tc => tc.Computer)
            .ToListAsync();
        }

        public async Task<Ticket?> ReadAsync(int id)
        {
            var ticket = await _db.Tickets
                .Include(t => t.Contact)
                .Include(t => t.TicketComputers)
                    .ThenInclude(tc => tc.Computer)
                .FirstOrDefaultAsync(t => t.Id == id);
            return ticket;
        }

        public async Task UpdateAsync(int id, Ticket ticket)
        {
            var existingTicket = await ReadAsync(id);
            if (existingTicket != null)
            {
                existingTicket.ContactId = ticket.ContactId;
                existingTicket.SubmittedAt = ticket.SubmittedAt;
                existingTicket.IsResolved = ticket.IsResolved;
                existingTicket.ChargerGiven = ticket.ChargerGiven;

                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<TicketDTO>> ReadAllDtoAsync()
        {
            return await _db.Tickets
                .Include(t => t.Contact) // Still need to EAGER LOAD for the data
                .Include(t => t.TicketComputers)
                    .ThenInclude(tc => tc.Computer) // Still need to EAGER LOAD the Computer
                .Select(t => new TicketDTO
                {
                    Id = t.Id,
                    SubmittedAt = t.SubmittedAt,
                    IsResolved = t.IsResolved,
                    // FLATTENING: pulling Contact Name directly into the DTO
                    ContactName = t.Contact.Name,

                    // PROJECTING THE COLLECTION: mapping the EF entities to DTOs
                    TicketComputers = t.TicketComputers.Select(tc => new TicketComputerDTO
                    {
                        // Assign join table properties here if any...
                        Computer = new ComputerDTO
                        {
                            Id = tc.Computer.Id,
                            AssetTag = tc.Computer.AssetTag,
                            Model = tc.Computer.Model
                        }
                    }).ToList()
                })
                .ToListAsync();
        }
    }
}