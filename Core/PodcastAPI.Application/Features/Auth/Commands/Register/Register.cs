using AutoMapper;
using MediatR;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Auth.Commands.Register
{
    public static class Register
    {
        public class Command : IRequest<Response>
        {
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
            public string ConfirmPassword { get; set; } = string.Empty;
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Username { get; set; } = string.Empty;
            public string Email { get; set; } = string.Empty;
            public string Token { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUserRepository _userRepository;
            private readonly IMapper _mapper;
            private readonly ITokenService _tokenService;

            public Handler(IUserRepository userRepository, IMapper mapper, ITokenService tokenService)
            {
                _userRepository = userRepository;
                _mapper = mapper;
                _tokenService = tokenService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.Password != request.ConfirmPassword) throw new ArgumentException("Passwords do not match.");

                var newUser = _mapper.Map<User>(request);
                newUser.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);

                await _userRepository.AddAsync(newUser);

                var token = _tokenService.GenerateToken(newUser);

                return new Response
                {
                    Id = newUser.Id,
                    Username = newUser.Username,
                    Email = newUser.Email,
                    Token = token
                };
            }

        }
    }
}
