using MediatR;
using PodcastAPI.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.Features.Auth.Commands.ResetPassword
{
    public static class ResetPassword
    {
        public class Command : IRequest<Response>
        {
            public string Email { get; set; } = string.Empty;
            public string ResetToken { get; set; } = string.Empty;
            public string NewPassword { get; set; } = string.Empty;
            public string ConfirmPassword { get; set; } = string.Empty;
        }
        public class Response
        {
            public bool Success { get; set; } = false;
            public string Message { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IUserRepository _userRepository;

            public Handler(IUserRepository userRepository)
            {
                _userRepository = userRepository;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                if (request.NewPassword != request.ConfirmPassword)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Passwords do not match."
                    };
                }

                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user == null || user.PasswordResetToken != request.ResetToken || user.PasswordResetTokenExpiryTime < DateTime.UtcNow)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "Invalid or expired reset token."
                    };
                }

                user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);

                user.PasswordResetToken = null;
                user.PasswordResetTokenExpiryTime = null;

                await _userRepository.UpdateAsync(user);

                return new Response
                {
                    Success = true,
                    Message = "Password has been reset successfully."
                };
            }
        }
    }
}
