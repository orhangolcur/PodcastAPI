using AutoMapper;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.DTOs.Podcast;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Services
{
    public class SubscriptionService : ISubscriptionService
    {
        private readonly ISubscriptionRepository _subscriptionRepository;
        private readonly IMapper _mapper;

        public SubscriptionService(ISubscriptionRepository subscriptionRepository, IMapper mapper)
        {
            _subscriptionRepository = subscriptionRepository;
            _mapper = mapper;
        }
        public async Task<List<PodcastDto>> GetUserSubscriptionsAsync(Guid userId)
        {
            var podcasts = _subscriptionRepository.GetUserSubscriptionsAsync(userId);
            return _mapper.Map<List<PodcastDto>>(podcasts);
        }

        public async Task<bool> ToogleSubscriptionAsync(Guid userId, Guid podcastId)
        {
            bool isSubscribed = await _subscriptionRepository.IsSubscribedAsync(userId, podcastId);
            if (isSubscribed)
            {
                await _subscriptionRepository.UnsubscribeAsync(userId, podcastId);
                return false;
            }
            else
            {
                var subscription = new Subscription
                {
                    UserId = userId,
                    PodcastId = podcastId,
                    SubscribedDate = DateTime.UtcNow
                };
                await _subscriptionRepository.SubscribeAsync(subscription);
                return true;
            }
        }
    }
}
