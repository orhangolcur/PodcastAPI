

using AutoMapper;
using MediatR;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Subscriptions.Queries
{
    public static class GetMySubscriptions
    {
        public class Query : IRequest<List<Response>>
        {
            public Guid UserId { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public bool IsTrend { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Response>>
        {
            private readonly ISubscriptionRepository _subscriptionRepository;
            private readonly IMapper _mapper;

            public Handler(ISubscriptionRepository subscriptionRepository, IMapper mapper)
            {
                _subscriptionRepository = subscriptionRepository;
                _mapper = mapper;
            }

            public async Task<List<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var podcasts = await _subscriptionRepository.GetUserSubscriptionsAsync(request.UserId);
                return _mapper.Map<List<Response>>(podcasts);
            }
        }
    }
}
