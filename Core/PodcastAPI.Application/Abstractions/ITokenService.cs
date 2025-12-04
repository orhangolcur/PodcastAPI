using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Application.Abstractions
{
    public interface ITokenService
    {
        string GenerateToken(User user);
    }
}
