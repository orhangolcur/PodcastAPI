using AutoMapper;
using MediatR;
using PodcastAPI.Domain.Interfaces;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById
{
    public class GetPodcastByIdQueryHandler : IRequestHandler<GetPodcastByIdQueryRequest, GetPodcastByIdQueryResponse>
    {
        private readonly IPodcastRepository _podcastRepository;
        private readonly IMapper _mapper;
        public GetPodcastByIdQueryHandler(IPodcastRepository podcastRepository, IMapper mapper)
        {
            _podcastRepository = podcastRepository;
            _mapper = mapper;
        }

        public async Task<GetPodcastByIdQueryResponse> Handle(GetPodcastByIdQueryRequest request, CancellationToken cancellationToken)
        {
            var podcast = await _podcastRepository.GetByIdAsync(request.Id);
            var response = _mapper.Map<GetPodcastByIdQueryResponse>(podcast);
            return response;
        }
    }
}
