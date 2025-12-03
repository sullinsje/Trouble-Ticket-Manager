using Trouble_Ticket_Manager.Models.Entities;
namespace Trouble_Ticket_Manager.Services
{
    public interface IUserRepository
    {
        Task<ICollection<User>> ReadAllAsync();
        Task<User?> ReadAsync(int id);
        Task<User> CreateAsync(User newUser);
        Task UpdateAsync(int id, User user);
        Task DeleteAsync(int id);
    }
}