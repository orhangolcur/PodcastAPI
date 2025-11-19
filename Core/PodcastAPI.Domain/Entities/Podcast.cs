namespace PodcastAPI.Domain.Entities
{
    public class Podcast
    {
        public Guid Id { get; set; }
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string ImageUrl { get; set; } = string.Empty;
        public string RssUrl { get; set; } = string.Empty;

        public ICollection<Episode> Episodes { get; set; } = new List<Episode>();
        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>();

    }
}
