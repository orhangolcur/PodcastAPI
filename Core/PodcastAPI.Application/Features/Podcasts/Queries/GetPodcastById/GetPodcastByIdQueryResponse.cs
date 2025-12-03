using PodcastAPI.Application.DTOs.Episode;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById
{
    public class GetPodcastByIdQueryResponse
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
