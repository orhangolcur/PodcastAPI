using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast;
using PodcastAPI.Application.Features.Podcasts.Queries.GetAllPodcasts;
using PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById;
using PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastsBySearch;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PodcastsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public PodcastsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllPodcasts([FromQuery] GetAllPodcast.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodcastById(Guid id)
        {
            var request = new GetPodcastById.Query { Id = id };

            var response = await _mediator.Send(request);

            if (response == null) return NotFound("Podcast Not Found!");

            return Ok(response);
        }


        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePodcast([FromBody] CreatePodcast.Command command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet("search")]
        public async Task<IActionResult> SearchPodcasts([FromQuery] GetPodcastsBySearch.Query query)
        {
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
