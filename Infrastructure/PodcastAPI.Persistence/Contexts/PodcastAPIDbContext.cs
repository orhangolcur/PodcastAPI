using Microsoft.EntityFrameworkCore;
using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Persistence.Contexts
{
    public class PodcastAPIDbContext : DbContext
    {
        public PodcastAPIDbContext(DbContextOptions<PodcastAPIDbContext> options) : base(options)
        {
        }

        public DbSet<Podcast> Podcasts { get; set; }
        public DbSet<Episode> Episodes { get; set; }
        public DbSet<Subscription> Subscriptions { get; set; }
        public DbSet<User> Users { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Podcast>()
                .HasMany(p => p.Episodes)
                .WithOne(e => e.Podcast)
                .HasForeignKey(e => e.PodcastId);   

            modelBuilder.Entity<Subscription>()
                .HasKey(s => new { s.UserId, s.PodcastId });

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.User)
                .WithMany(u => u.Subscriptions)
                .HasForeignKey(s => s.UserId);

            modelBuilder.Entity<Subscription>()
                .HasOne(s => s.Podcast)
                .WithMany(p => p.Subscriptions)
                .HasForeignKey(s => s.PodcastId);

        }
    }
}
