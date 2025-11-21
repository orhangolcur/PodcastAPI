using PodcastAPI.Application.DTOs.Podcast;

namespace PodcastAPI.Application.Abstractions
{
    public interface ISubscriptionService
    {
        Task<List<PodcastDto>> GetUserSubscriptionsAsync(Guid userId);
        Task<bool> ToogleSubscriptionAsync(Guid userId, Guid podcastId);
    }
}
