using MediatR;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Users.Commands.UpdateProfile
{
    public static class UpdateProfile
    {
        public class Command : IRequest<Response>
        {
            public Guid UserId { get; set; }
            public string Username { get; set; }
            public string Bio { get; set; }
            public string ImageUrl { get; set; }
        }

        public class Response
        {
            public bool Success { get; set; }
            public string Message { get; set; }
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
                var user = await _userRepository.GetByIdAsync(request.UserId);
                if (user == null) throw new Exception("User not found");

                if (!string.IsNullOrWhiteSpace(request.Username))
                {
                    user.Username = request.Username;
                }

                if (!string.IsNullOrWhiteSpace(request.Bio))
                {
                    user.Bio = request.Bio;
                }

                if (!string.IsNullOrWhiteSpace(request.ImageUrl))
                {
                    user.ImageUrl = request.ImageUrl;
                }

                await _userRepository.UpdateAsync(user);

                return new Response
                {
                    Success = true,
                    Message = "Profile updated successfully"
                };

            }
        }
    }
}
