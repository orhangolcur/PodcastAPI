using PodcastAPI.Domain.Entities;

namespace PodcastAPI.Application.Abstractions
{
    public interface IRssService
    {
        Task<List<Episode>> GetEpisodesFromRss(string rssUrl, Guid podcastId);
    }
}
