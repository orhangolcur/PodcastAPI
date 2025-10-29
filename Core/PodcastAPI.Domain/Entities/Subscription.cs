namespace PodcastAPI.Domain.Entities
{
    public class Subscription
    {
        public int UserId { get; set; }
        public User User { get; set; } = null!;

        public int PodcastId { get; set; }
        public Podcast Podcast { get; set; } = null!;

        public DateTime SubscribedDate { get; set; } = DateTime.UtcNow;
    }
}
