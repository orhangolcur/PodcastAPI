using AutoMapper;
using MediatR;
using PodcastAPI.Application.DTOs.Podcast;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetAllPodcasts
{
    public class GetAllPodcastsQueryHandler : IRequestHandler<GetAllPodcastsQueryRequest, List<GetAllPodcastsQueryResponse>>
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IMapper _mapper;
        public GetAllPodcastsQueryHandler(IPodcastRepository podcastRepository, IMapper mapper)
        {
            _podcastRepository = podcastRepository;
            _mapper = mapper;
        }
        public async Task<List<GetAllPodcastsQueryResponse>> Handle(GetAllPodcastsQueryRequest request, CancellationToken cancellationToken)
        {
            var podcasts = await _podcastRepository.GetAllAsync();
            return _mapper.Map<List<GetAllPodcastsQueryResponse>>(podcasts);
        }
    }
}
