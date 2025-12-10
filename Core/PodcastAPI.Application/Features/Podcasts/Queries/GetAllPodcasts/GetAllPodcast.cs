using AutoMapper;
using MediatR;
using PodcastAPI.Application.DTOs.Episode;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetAllPodcasts
{
    public static class GetAllPodcast
    {
        public class  Query : IRequest<List<Response>>
        {}

        public class Response
        {
            public Guid Id { get; set; }
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string Category { get; set; } = string.Empty;
            public bool IsTrend { get; set; }
            public List<EpisodeDto> Episodes { get; set; }
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
                var podcasts = await _podcastRepository.GetAllAsync();
                return _mapper.Map<List<Response>>(podcasts);
            }
        }
    }
}
