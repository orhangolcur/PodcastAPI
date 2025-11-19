using Microsoft.EntityFrameworkCore;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;
using PodcastAPI.Persistence.Contexts;

namespace PodcastAPI.Persistence.Repositories
{
    public class PodcastRepository : IPodcastRepository
    {
        private readonly PodcastAPIDbContext _context;

        public PodcastRepository(PodcastAPIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Podcast podcast)
        {
            await _context.Podcasts.AddAsync(podcast);
            await _context.SaveChangesAsync();
        }

        public async Task<List<Podcast>> GetAllAsync()
        {
            // Eagerly load Episodes and Subscriptions
            return await _context.Podcasts
                .Include(p => p.Episodes)
                .ToListAsync();
        }

        public async Task<Podcast?> GetByIdAsync(Guid id)
        {
            return await _context.Podcasts
                .Include(p => p.Episodes)
                .FirstOrDefaultAsync(p => p.Id == id);
        }
    }
}
