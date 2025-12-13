using MediatR;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Domain.Interfaces;
using System.Security.Cryptography;

namespace PodcastAPI.Application.Features.Auth.Commands.ForgotPassword
{
    public static class ForgotPassword
    {
        public class Command : IRequest<Response>
        {
            public string Email { get; set; } = string.Empty;
        }

        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IEmailService _emailService;
            private readonly IUserRepository _userRepository;

            public Handler(IEmailService emailService, IUserRepository userRepository)
            {
                _emailService = emailService;
                _userRepository = userRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var user = await _userRepository.GetByEmailAsync(request.Email);

                if (user == null)
                {
                    return new Response
                    {
                        Success = false,
                        Message = "If an account with that email exists, a password reset link has been sent."
                    };
                }

                var resetToken = RandomNumberGenerator.GetInt32(100000, 999999).ToString();

                user.PasswordResetToken = resetToken;
                user.PasswordResetTokenExpiryTime = DateTime.UtcNow.AddMinutes(15);

                await _userRepository.UpdateAsync(user);

                var emailBody = $"Your password reset code is: {resetToken}. It will expire in 15 minutes.";
                await _emailService.SendEmailAsync(user.Email, "Password Reset Request", emailBody);

                return new Response
                {
                    Success = true,
                    Message = "Password reset code has been sent."
                };
            }
        }
    }
}
