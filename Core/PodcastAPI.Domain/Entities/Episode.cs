namespace PodcastAPI.Domain.Entities
{
    public class Episode
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string AudioUrl { get; set; } = string.Empty;
        public DateTime PublishedDate { get; set; }
        public TimeSpan Duration { get; set; }

        public Guid PodcastId { get; set; }
        public Podcast Podcast { get; set; } = null!;

    }
}
