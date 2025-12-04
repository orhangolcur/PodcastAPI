

using MediatR;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Subscriptions.Commands
{
    public static class ToggleSubscription
    {
        public class Command : IRequest<Response>
        {
            public Guid UserId { get; set; }
            public Guid PodcastId { get; set; }
        }

        public class Response
        {
            public bool IsSubscribed { get; set; }
            public string Message { get; set; } = string.Empty;
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;

            public Handler(ISubscriptionRepository subscriptionRepository)
            {
                _subscriptionRepository = subscriptionRepository;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                bool isSubscribed = await _subscriptionRepository.IsSubscribedAsync(request.UserId, request.PodcastId);

                if (isSubscribed)
                {
                    await _subscriptionRepository.UnsubscribeAsync(request.UserId, request.PodcastId);
                    return new Response
                    {
                        IsSubscribed = false,
                        Message = "Unsubscribed successfully."
                    };
                }
                else
                {
                    var newSub = new Subscription
                    {
                        UserId = request.UserId,
                        PodcastId = request.PodcastId,
                        SubscribedDate = DateTime.UtcNow
                    };
                    await _subscriptionRepository.SubscribeAsync(newSub);
                    return new Response
                    {
                        IsSubscribed = true,
                        Message = "Subscribed successfully."
                    };
                }
            }
        }   
    }
}
