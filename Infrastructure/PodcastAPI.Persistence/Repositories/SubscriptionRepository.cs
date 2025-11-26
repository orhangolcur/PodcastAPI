using Microsoft.EntityFrameworkCore;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;
using PodcastAPI.Persistence.Contexts;

namespace PodcastAPI.Persistence.Repositories
{
    public class SubscriptionRepository : ISubscriptionRepository
    {
        private readonly PodcastAPIDbContext _context;

        public SubscriptionRepository(PodcastAPIDbContext context)
        {
            _context = context;
        }

        public async Task<List<Podcast>> GetUserSubscriptionsAsync(Guid userId)
        {
            return await _context.Podcasts
                .Where(p => p.Subscriptions.Any(s => s.UserId == userId))
                .Include(p => p.Episodes)
                .ToListAsync();
        }

        public async Task<bool> IsSubscribedAsync(Guid userId, Guid podcastId)
        {
            return await _context.Subscriptions
                .AnyAsync(s => s.UserId == userId && s.PodcastId == podcastId);
        }

        public async Task SubscribeAsync(Subscription subscription)
        {
            await _context.Subscriptions.AddAsync(subscription);
            await _context.SaveChangesAsync();
        }

        public async Task UnsubscribeAsync(Guid userId, Guid podcastId)
        {
            var subscription = await _context.Subscriptions
                .FirstOrDefaultAsync(s => s.UserId == userId && s.PodcastId == podcastId);

            if (subscription != null)
            {
                _context.Subscriptions.Remove(subscription);
                await _context.SaveChangesAsync();
            }
        }
    }
}
