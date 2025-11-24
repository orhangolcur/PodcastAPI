using PodcastAPI.Application.Abstractions;
using PodcastAPI.Domain.Entities;
using System.ServiceModel.Syndication;
using System.Xml;

namespace PodcastAPI.Infrastructure.Services
{
    public class RssService : IRssService
    {
        public async Task<List<Episode>> GetEpisodesFromRss(string rssUrl, Guid podcastId)
        {
            return await Task.Run(() =>
            {
                var episodes = new List<Episode>();

                try
                {
                    var settings = new XmlReaderSettings
                    {
                        DtdProcessing = DtdProcessing.Parse
                    };

                    using var reader = XmlReader.Create(rssUrl, settings);
                    var feed = SyndicationFeed.Load(reader);

                    foreach (var item in feed.Items)
                    {
                        var audioUrl = item.Links.FirstOrDefault(l =>
                            l.RelationshipType == "enclosure" &&
                            (l.MediaType?.Contains("audio") == true || l.Uri.ToString().EndsWith(".mp3")))?.Uri.ToString();

                        if (string.IsNullOrEmpty(audioUrl)) continue;

                        TimeSpan duration = TimeSpan.Zero;
                        var durationExtension = item.ElementExtensions
                            .FirstOrDefault(e => e.OuterName == "duration" && e.OuterNamespace == "http://www.itunes.com/dtds/podcast-1.0.dtd");

                        if (durationExtension != null)
                        {
                            try
                            {
                                var durationString = durationExtension.GetObject<string>();
                                if (int.TryParse(durationString, out int seconds))
                                {
                                    duration = TimeSpan.FromSeconds(seconds);
                                }
                                else if (TimeSpan.TryParse(durationString, out TimeSpan parsedDuration))
                                {
                                    duration = parsedDuration;
                                }
                            }
                            catch
                            {
                            }
                        }

                        DateTime pubDate = item.PublishDate.DateTime;

                        if (pubDate == DateTime.MinValue)
                        {
                            pubDate = item.LastUpdatedTime.DateTime;
                        }

                        if (pubDate == DateTime.MinValue)
                        {
                            pubDate = DateTime.UtcNow;
                        }

                        var episode = new Episode
                        {
                            Id = Guid.NewGuid(),
                            Title = item.Title?.Text ?? "Başlıksız Bölüm",
                            Description = item.Summary?.Text ?? string.Empty,
                            AudioUrl = audioUrl,
                            PublishedDate = pubDate,
                            Duration = duration,
                            PodcastId = podcastId
                        };
                        episodes.Add(episode);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"RSS beslemesi alınırken hata oluştu: {ex.Message}");
                    return new List<Episode>();
                }

                return episodes;
            });
        }
    }
}