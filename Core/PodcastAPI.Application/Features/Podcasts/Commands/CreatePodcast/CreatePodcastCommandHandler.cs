using AutoMapper;
using MediatR;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.DTOs.Podcast;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast
{
    public class CreatePodcastCommandHandler : IRequestHandler<CreatePodcastCommandRequest, CreatePodcastCommandResponse>
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IRssService _rssService;
        private readonly IMapper _mapper;
        public CreatePodcastCommandHandler(IPodcastRepository podcastRepository, IRssService rssService, IMapper mapper)
        {
            _podcastRepository = podcastRepository;
            _rssService = rssService;
            _mapper = mapper;
        }
        public async Task<CreatePodcastCommandResponse> Handle(CreatePodcastCommandRequest request, CancellationToken cancellationToken)
        {
            var podcastEntity = _mapper.Map<Podcast>(request);
            podcastEntity.Id = Guid.NewGuid();

            if (!string.IsNullOrEmpty(request.RssUrl))
            {
                var episodes = await _rssService.GetEpisodesFromRss(request.RssUrl, podcastEntity.Id);
                if (episodes.Count > 0) podcastEntity.Episodes = episodes;
            }

            await _podcastRepository.AddAsync(podcastEntity);
            var podcastDto = _mapper.Map<CreatePodcastCommandResponse>(podcastEntity);
            return podcastDto;
        }
    }
}
