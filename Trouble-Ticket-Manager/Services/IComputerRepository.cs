using Trouble_Ticket_Manager.Models.Entities;

namespace Trouble_Ticket_Manager.Services
{
    public interface IComputerRepository
    {
        Task<ICollection<Computer>> ReadAllAsync();
        Task<Computer?> ReadAsync(string assetTag);
        Task<Computer> CreateAsync(Computer newComputer);
        Task UpdateAsync(string assetTag, Computer computer);
        Task DeleteAsync(string assetTag);
    }
}