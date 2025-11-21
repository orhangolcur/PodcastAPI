using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Domain.Interfaces
{
    public interface ISubscriptionRepository
    {
        Task<bool> IsSubscribedAsync(Guid userId, Guid podcastId);
        Task SubscribeAsync(Subscription subscription);
        Task UnsubscribeAsync(Guid userId, Guid podcastId);  
        Task<List<Podcast>> GetUserSubscriptionsAsync(Guid userId);
    }
}
