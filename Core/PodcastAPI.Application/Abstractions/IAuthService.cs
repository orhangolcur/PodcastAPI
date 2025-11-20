using PodcastAPI.Application.DTOs.Auth;

namespace PodcastAPI.Application.Abstractions
{
    public interface IAuthService
    {
        Task<AuthResponse> RegisterAsync(RegisterRequest registerRequest);
        Task<AuthResponse?> LoginAsync(LoginRequest loginRequest);
    }
}
