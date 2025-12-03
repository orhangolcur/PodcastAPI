using MediatR;
using PodcastAPI.Application.DTOs.Podcast;

namespace PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast
{
    public class CreatePodcastCommandRequest : IRequest<CreatePodcastCommandResponse>
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsTrend { get; set; } = false;
        public string RssUrl { get; set; } = string.Empty;
    }
}
