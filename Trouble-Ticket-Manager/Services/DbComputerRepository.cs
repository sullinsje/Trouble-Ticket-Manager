using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public class DbComputerRepository : IComputerRepository
    {
        private readonly ApplicationDbContext _db;
        public DbComputerRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Computer> CreateAsync(Computer newComputer)
        {
            await _db.Computers.AddAsync(newComputer);
            await _db.SaveChangesAsync();
            return newComputer;
        }

        public Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<ICollection<Computer>> ReadAllAsync()
        {
            return await _db.Computers
                .Include(c => c.User)
                .ToListAsync();
        }

        public Task<Computer?> ReadAsync(int id)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(int id, Computer computer)
        {
            throw new NotImplementedException();
        }
    }
}