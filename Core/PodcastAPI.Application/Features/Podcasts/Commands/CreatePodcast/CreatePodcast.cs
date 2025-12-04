using AutoMapper;
using MediatR;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast
{
    public static class CreatePodcast
    {
        public class Command : IRequest<Response>
        {
            public string Title { get; set; } = string.Empty;
            public string Description { get; set; } = string.Empty;
            public string ImageUrl { get; set; } = string.Empty;
            public string RssUrl { get; set; } = string.Empty;
            public string Category { get; set; } = "All";
            public bool IsTrend { get; set; } = false;
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

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IPodcastRepository _podcastRepository;
            private readonly IRssService _rssService;
            private readonly IMapper _mapper;
            public Handler(IPodcastRepository podcastRepository, IRssService rssService, IMapper mapper)
            {
                _podcastRepository = podcastRepository;
                _rssService = rssService;
                _mapper = mapper;
            }
            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var podcastEntity = _mapper.Map<Podcast>(request);
                podcastEntity.Id = Guid.NewGuid();

                if (!string.IsNullOrEmpty(request.RssUrl))
                {
                    var episodes = await _rssService.GetEpisodesFromRss(request.RssUrl, podcastEntity.Id);
                    if (episodes.Count > 0) podcastEntity.Episodes = episodes;
                }

                await _podcastRepository.AddAsync(podcastEntity);
                var podcastDto = _mapper.Map<Response>(podcastEntity);
                return podcastDto;
            }
        }
    }
}
