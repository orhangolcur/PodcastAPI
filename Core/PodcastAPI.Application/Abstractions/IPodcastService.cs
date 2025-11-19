using PodcastAPI.Application.DTOs.Podcast;

namespace PodcastAPI.Application.Abstractions
{
    public interface IPodcastService
    {
        Task<List<PodcastDto>> GetAllPodcastsAsync();
        Task<PodcastDto?> GetPodcastByIdAsync(Guid id);
        Task<PodcastDto> CreatePodcastAsync(CreatePodcastRequest createPodcastDto);
    }
}
