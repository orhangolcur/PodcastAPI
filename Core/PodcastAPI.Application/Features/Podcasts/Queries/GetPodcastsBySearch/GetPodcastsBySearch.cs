using AutoMapper;
using MediatR;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastsBySearch
{
    public static class GetPodcastsBySearch
    {
        public class Query : IRequest<List<Response>>
        {
            public string SearchTerm { get; set; } = string.Empty;
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string Category { get; set; }
            public bool IsTrend { get; set; }
        }

        public class Handler : IRequestHandler<Query, List<Response>>
        {
            private readonly IPodcastRepository _podcastRepository;
            private readonly IMapper _mapper;
            public Handler(IPodcastRepository podcastRepository, IMapper mapper)
            {
                _podcastRepository = podcastRepository;
                _mapper = mapper;
            }
            public async Task<List<Response>> Handle(Query request, CancellationToken cancellationToken)
            {
                var podcasts = await _podcastRepository.SearchAsync(request.SearchTerm);
                return _mapper.Map<List<Response>>(podcasts);
            }
        }
    }
}
