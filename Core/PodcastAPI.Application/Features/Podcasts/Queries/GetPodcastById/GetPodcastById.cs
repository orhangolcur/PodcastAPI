using AutoMapper;
using MediatR;
using PodcastAPI.Application.DTOs.Episode;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById
{
    public static class GetPodcastById
    {
        public class Query : IRequest<Response>
        {
            public Guid Id { get; set; }
        }

        public class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public bool IsTrend { get; set; }
            public List<EpisodeDto> Episodes { get; set; } = new List<EpisodeDto>();
        }

        public class Handler : IRequestHandler<Query, Response>
        {
            private readonly IPodcastRepository _podcastRepository;
            private readonly IMapper _mapper;
            public Handler(IPodcastRepository podcastRepository, IMapper mapper)
            {
                _podcastRepository = podcastRepository;
                _mapper = mapper;
            }

            public async Task<Response> Handle(Query request, CancellationToken cancellationToken)
            {
                var podcast = await _podcastRepository.GetByIdAsync(request.Id);
                var response = _mapper.Map<Response>(podcast);
                return response;

            }
        }
    }
}
