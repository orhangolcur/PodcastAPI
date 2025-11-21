using PodcastAPI.Application.DTOs.Episode;

namespace PodcastAPI.Application.DTOs.Podcast
{
    public class PodcastDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string Category { get; set; } = string.Empty;
        public bool IsTrend { get; set; } 

        public List<EpisodeDto> Episodes { get; set; } = new List<EpisodeDto>();
    }
}
