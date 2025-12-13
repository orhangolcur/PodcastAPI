using MediatR;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.Features.Auth.Commands.RefreshToken
{
    public static class RefreshToken
    {
        public class  Command : IRequest<Response>
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
        }

        public class Response
        {
            public string AccessToken { get; set; }
            public string RefreshToken { get; set; }
            public bool Success { get; set; }
            public string Message { get; set; }

        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly ITokenService _tokenService;

            public Handler(ITokenService tokenService, IUserRepository userRepository)
            {
                _tokenService = tokenService;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByRefreshTokenAsync(request.RefreshToken);

                if (user == null || user.RefreshTokenExpiryTime <= DateTime.Now)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Oturum süresi dolmuş, tekrar giriş yapın."
                    };
                }

                var newAccessToken = _tokenService.GenerateToken(user);
                var newRefreshToken = _tokenService.GenerateRefreshToken();

                user.RefreshToken = newRefreshToken;
                user.RefreshTokenExpiryTime = DateTime.Now.AddDays(7);

                await _userRepository.UpdateAsync(user);

                return new Response
                {
                    AccessToken = newAccessToken,
                    RefreshToken = newRefreshToken,
                    Success = true,
                    Message = "Token başarıyla yenilendi."
                };

            }
        }
    }
}
