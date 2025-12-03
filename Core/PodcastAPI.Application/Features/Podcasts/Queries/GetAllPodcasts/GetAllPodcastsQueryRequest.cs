using MediatR;
using PodcastAPI.Application.DTOs.Podcast;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetAllPodcasts
{
    public class GetAllPodcastsQueryRequest : IRequest<List<GetAllPodcastsQueryResponse>>
    {
    }
}
