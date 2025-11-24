using AutoMapper;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.DTOs.Podcast;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Services
{
    public class PodcastService : IPodcastService
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IMapper _mapper;

        private readonly IRssService _rssService;

        public PodcastService(IMapper mapper, IPodcastRepository podcastRepository, IRssService rssService)
        {
            _mapper = mapper;
            _podcastRepository = podcastRepository;
            _rssService = rssService;
        }

        public async Task<PodcastDto> CreatePodcastAsync(CreatePodcastRequest createPodcastDto)
        {
            var podcastEntity = _mapper.Map<Podcast>(createPodcastDto);

            podcastEntity.Id = Guid.NewGuid();  

            if (!string.IsNullOrEmpty(createPodcastDto.RssUrl))
            {
                var episodes = await _rssService.GetEpisodesFromRss(createPodcastDto.RssUrl, podcastEntity.Id);
                if (episodes.Count > 0)
                {
                    podcastEntity.Episodes = episodes;
                }   
            }
            await _podcastRepository.AddAsync(podcastEntity);
            var podcastDto = _mapper.Map<PodcastDto>(podcastEntity);
            return podcastDto;

        }

        public async Task<List<PodcastDto>> GetAllPodcastsAsync()
        {
            var podcasts = await _podcastRepository.GetAllAsync();
            var podcastDtos = _mapper.Map<List<PodcastDto>>(podcasts);
            return podcastDtos;
        }

        public async Task<PodcastDto?> GetPodcastByIdAsync(Guid id)
        {
            var podcast = await _podcastRepository.GetByIdAsync(id);
            if (podcast == null)
                return null;
            
            var podcastDto = _mapper.Map<PodcastDto>(podcast);
            return podcastDto;
        }
    }
}
