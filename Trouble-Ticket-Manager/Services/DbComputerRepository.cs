using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public class DbComputerRepository : IComputerRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IUserRepository _userRepo;
        public DbComputerRepository(ApplicationDbContext db, IUserRepository userRepo)
        {
            _db = db;
            _userRepo = userRepo;
        }

        public async Task<Computer> CreateAsync(Computer newComputer)
        {
            newComputer.UnderWarranty = MockWarrantyChecker.GetMockWarranty(newComputer.ServiceTag);
            await _db.Computers.AddAsync(newComputer);
            await _db.SaveChangesAsync();
            return newComputer;
        }

        public async Task DeleteAsync(string assetTag)
        {
            var computerToDelete = await ReadAsync(assetTag);
            if (computerToDelete != null)
            {
                _db.Computers.Remove(computerToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Computer>> ReadAllAsync()
        {
            return await _db.Computers
                .Include(c => c.TicketComputers)
                .Include(c => c.User)
                .ToListAsync();
        }

        public async Task<Computer?> ReadAsync(string assetTag)
        {
            return await _db.Computers
                .Include(c => c.User)
                .Include(c => c.TicketComputers)
                .FirstOrDefaultAsync(c => c.AssetTag == assetTag);
        }

        public async Task UpdateAsync(string assetTag, Computer computer)
        {
            var existingComputer = await ReadAsync(assetTag);
            if (existingComputer != null)
            {
                existingComputer.AssetTag = computer.AssetTag;
                existingComputer.ServiceTag = computer.ServiceTag;
                existingComputer.Model = computer.Model;
                existingComputer.UserId = computer.UserId;
                existingComputer.UnderWarranty = computer.UnderWarranty;

                await _db.SaveChangesAsync();
            }
        }
    }
}