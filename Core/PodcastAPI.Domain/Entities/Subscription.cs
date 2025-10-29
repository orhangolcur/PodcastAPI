namespace PodcastAPI.Domain.Entities
{
    public class Subscription
    {
        public Guid UserId { get; set; }
        public User User { get; set; } = null!;

        public Guid PodcastId { get; set; }
        public Podcast Podcast { get; set; } = null!;

        public DateTime SubscribedDate { get; set; } = DateTime.UtcNow;
    }
}
