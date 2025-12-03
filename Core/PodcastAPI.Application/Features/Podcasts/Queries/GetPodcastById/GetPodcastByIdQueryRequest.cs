using MediatR;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById
{
    public class GetPodcastByIdQueryRequest : IRequest<GetPodcastByIdQueryResponse>
    {
        public Guid Id { get; set; }

        public GetPodcastByIdQueryRequest(Guid id)
        {
            Id = id;
        }
    }
}
