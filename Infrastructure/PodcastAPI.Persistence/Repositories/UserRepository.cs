using Microsoft.EntityFrameworkCore;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;
using PodcastAPI.Persistence.Contexts;

namespace PodcastAPI.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly PodcastAPIDbContext _context;

        public UserRepository(PodcastAPIDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(User user)
        {
            await _context.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User?> GetByEmailAsync(string username)
        {
            return await _context.Users
                .FirstOrDefaultAsync(u => u.Email == username);
        }

        public async Task<User?> GetByIdAsync(Guid id)
        {
            return await _context.Users.FindAsync(id);
        }

        public async Task UpdateAsync(User user)
        {
            _context.Users.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
