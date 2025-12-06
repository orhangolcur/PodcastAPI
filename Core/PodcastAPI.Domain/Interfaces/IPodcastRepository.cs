using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Domain.Interfaces
{
    public interface IPodcastRepository
    {
        Task<Podcast?> GetByIdAsync(Guid id);
        Task<List<Podcast>> GetAllAsync();
        Task AddAsync(Podcast podcast);
        Task<List<Podcast>> SearchAsync(string searchTerm);
    }
}
