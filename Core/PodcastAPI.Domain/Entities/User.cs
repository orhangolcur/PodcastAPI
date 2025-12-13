namespace PodcastAPI.Domain.Entities
{
    public class User
    {
        public Guid Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public string PasswordHash { get; set; } = string.Empty;
        public string? Bio { get; set; } = string.Empty;
        public string? ImageUrl { get; set; } = string.Empty;
        public string? RefreshToken { get; set; } = string.Empty;
        public DateTime? RefreshTokenExpiryTime { get; set; } = null;
        public string? PasswordResetToken { get; set; } = string.Empty;
        public DateTime? PasswordResetTokenExpiryTime { get; set; } = null;

        public ICollection<Subscription> Subscriptions { get; set; } = new List<Subscription>(); 
    }
}
