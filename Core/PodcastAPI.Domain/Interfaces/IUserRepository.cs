using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Domain.Interfaces
{
    public interface IUserRepository
    {
        Task<User?> GetByIdAsync(Guid id);
        Task<User?> GetByEmailAsync(string username);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
    }
}
