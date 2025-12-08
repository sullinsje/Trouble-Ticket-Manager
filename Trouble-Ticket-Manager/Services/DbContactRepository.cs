using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public class DbContactRepository : IContactRepository
    {
        private readonly ApplicationDbContext _db;
        public DbContactRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<Contact> CreateAsync(Contact newContact)
        {
            await _db.Contacts.AddAsync(newContact);
            await _db.SaveChangesAsync();
            return newContact;
        }

        public async Task DeleteAsync(int id)
        {
            var contactToDelete = await ReadAsync(id);
            if (contactToDelete != null)
            {
                _db.Contacts.Remove(contactToDelete);
                await _db.SaveChangesAsync();
            }
        }

        public async Task<ICollection<Contact>> ReadAllAsync()
        {
            return await _db.Contacts.ToListAsync();
        }

        public async Task<Contact?> ReadAsync(int id)
        {
            return await _db.Contacts.Include(u => u.Computers).FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task UpdateAsync(int id, Contact contact)
        {
            var existingContact = await ReadAsync(id);
            if (existingContact != null)
            {
                existingContact.Name = contact.Name;
                existingContact.Email = contact.Email;
                existingContact.Building = contact.Building;
                existingContact.Room = contact.Room;

                await _db.SaveChangesAsync();
            }
        }
    
    }
}