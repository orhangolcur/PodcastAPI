using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PodcastAPI.Application.DTOs.Episode
{
    public class EpisodeDto
    {
        public Guid Id { get; set; }
        public string Title { get; set; }= string.Empty;
        public string Description { get; set; }= string.Empty;
        public string AudioUrl { get; set; }= string.Empty;
        public double DurationMinutes { get; set; }
        public DateTime PublishDate { get; set; }

    }
}
