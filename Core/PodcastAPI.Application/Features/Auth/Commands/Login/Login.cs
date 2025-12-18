using MediatR;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace PodcastAPI.Application.Features.Auth.Commands.Login
{
    public static class Login
    {
        public class Command : IRequest<Response?>
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }
        public class Response
        {
            public Guid Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Bio { get; set; }
            public string ImageUrl { get; set; }
            public string Token { get; set; } = string.Empty;
            public string RefreshToken { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenService _tokenService;
            public Handler(IUserRepository userRepository, ITokenService tokenService)
            {
                _userRepository = userRepository;
                _tokenService = tokenService;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);
                if (user == null) throw new Exception("User Not Found!");

                var isPasswordValid = BCrypt.Net.BCrypt.Verify(request.Password, user.PasswordHash);
                if (!isPasswordValid) return null!;

                var token = _tokenService.GenerateToken(user);

                var refreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = refreshToken;
                user.RefreshTokenExpiryTime = DateTime.UtcNow.AddDays(7);

                await _userRepository.UpdateAsync(user);

                return new Response
                {
                    Id = user.Id,
                    Username = user.Username,
                    Email = user.Email,
                    Bio = user.Bio ?? "",
                    ImageUrl = user.ImageUrl ?? "",
                    Token = token,
                    RefreshToken = refreshToken
                };
            }
        }

    }
}
