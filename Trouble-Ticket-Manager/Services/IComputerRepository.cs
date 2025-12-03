using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public interface IComputerRepository
    {
        Task<ICollection<Computer>> ReadAllAsync();
        Task<Computer?> ReadAsync(int id);
        Task<Computer> CreateAsync(Computer newComputer);
        Task UpdateAsync(int id, Computer computer);
        Task DeleteAsync(int id);
    }
}