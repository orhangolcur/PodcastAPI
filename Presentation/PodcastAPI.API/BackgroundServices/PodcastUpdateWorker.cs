using Microsoft.EntityFrameworkCore;
using PodcastAPI.Domain.Entities;
using PodcastAPI.Persistence.Contexts;
using System.ServiceModel.Syndication;
using System.Xml;

namespace PodcastAPI.API.BackgroundServices
{
    public class PodcastUpdateWorker : BackgroundService
    {
        private readonly IServiceScopeFactory _serviceScopeFactory;
        private readonly ILogger<PodcastUpdateWorker> _logger;

        public PodcastUpdateWorker(IServiceScopeFactory serviceScopeFactory, ILogger<PodcastUpdateWorker> logger)
        {
            _serviceScopeFactory = serviceScopeFactory;
            _logger = logger;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            _logger.LogInformation("Podcast Güncelleme Servisi Başlatıldı...");

            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    _logger.LogInformation("Yeni bölümler taranıyor... ({Time})", DateTime.Now);

                    await UpdatePodcastsAsync();

                    _logger.LogInformation("Tarama bitti. 1 saat uyku moduna geçiliyor.");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Podcast güncelleme sırasında hata oluştu!");
                }

                await Task.Delay(TimeSpan.FromHours(1), stoppingToken);
            }
        }

        private async Task UpdatePodcastsAsync()
        {
            using (var scope = _serviceScopeFactory.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PodcastAPIDbContext>();

                var podcasts = await dbContext.Podcasts
                                              .Include(p => p.Episodes)
                                              .ToListAsync();

                foreach (var podcast in podcasts)
                {
                    if (string.IsNullOrEmpty(podcast.RssUrl)) continue;

                    try
                    {
                        using var reader = XmlReader.Create(podcast.RssUrl);
                        var feed = SyndicationFeed.Load(reader);

                        bool newEpisodeAdded = false;

                        foreach (var item in feed.Items)
                        {
                            var exists = podcast.Episodes.Any(e => e.Title == item.Title.Text);

                            if (!exists)
                            {
                                var newEpisode = new Episode
                                {
                                    Id = Guid.NewGuid(),
                                    PodcastId = podcast.Id,
                                    Title = item.Title.Text,
                                    Description = item.Summary?.Text ?? item.Title.Text,
                                    PublishedDate = item.PublishDate.DateTime,
                                    AudioUrl = item.Links.FirstOrDefault(l => l.RelationshipType == "enclosure")?.Uri.ToString() ?? "",
                                    Duration = TimeSpan.FromMinutes(30)
                                };

                                dbContext.Episodes.Add(newEpisode);
                                newEpisodeAdded = true;
                                _logger.LogInformation($"YENİ BÖLÜM EKLENDİ: {newEpisode.Title}");
                            }
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogWarning($"RSS Okuma Hatası ({podcast.Title}): {ex.Message}");
                    }
                }
                await dbContext.SaveChangesAsync();
            }
        }
    }
}