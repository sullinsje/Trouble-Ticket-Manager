using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public class DbUserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _db;
        public DbUserRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<User> CreateAsync(User newUser)
        {
            await _db.Users.AddAsync(newUser);
            await _db.SaveChangesAsync();
            return newUser;
        }

        public async Task DeleteAsync(int id)
        {
            var userToDelete = await ReadAsync(id);
            if (userToDelete != null)
            {
                _db.Users.Remove(userToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<User>> ReadAllAsync()
        {
            return await _db.Users.ToListAsync();
        }

        public async Task<User?> ReadAsync(int id)
        {
            return await _db.Users.Include(u => u.Computers).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(int id, User user)
        {
            var existingUser = await ReadAsync(id);
            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.Email = user.Email;
                existingUser.Building = user.Building;
                existingUser.Room = user.Room;
                // existingComputer.User = await _userRepo.ReadAsync(computer.UserId);

                await _db.SaveChangesAsync();
            }
        }
    
    }
}