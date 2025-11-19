using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.DTOs.Podcast;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastsController : ControllerBase
    {
        private readonly IPodcastService _podcastService;
        public PodcastsController(IPodcastService podcastService)
        {
            _podcastService = podcastService;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPodcasts()
        {
            var podcasts = await _podcastService.GetAllPodcastsAsync();
            return Ok(podcasts);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodcastById(Guid id)
        {
            var podcast = await _podcastService.GetPodcastByIdAsync(id);
            if (podcast == null)
                return NotFound("Böyle bir podcast bulunamadı!");
            return Ok(podcast);
        }

        [HttpPost]
        public async Task<IActionResult> CreatePodcast([FromBody] CreatePodcastRequest createPodcastDto)
        {
            if(string.IsNullOrEmpty(createPodcastDto.Title))
                return BadRequest("Podcast başlığı boş olamaz!");

            var createdPodcast = await _podcastService.CreatePodcastAsync(createPodcastDto);
            return CreatedAtAction(nameof(GetPodcastById), new { id = createdPodcast.Id }, createdPodcast);
        }
    }
}
