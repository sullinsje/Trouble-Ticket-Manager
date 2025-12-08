using Trouble_Ticket_Manager.Models.Entities;
namespace Trouble_Ticket_Manager.Services
{
    public interface IContactRepository
    {
        Task<ICollection<Contact>> ReadAllAsync();
        Task<Contact?> ReadAsync(int id);
        Task<Contact> CreateAsync(Contact newContact);
        Task UpdateAsync(int id, Contact contact);
        Task DeleteAsync(int id);
    }
}