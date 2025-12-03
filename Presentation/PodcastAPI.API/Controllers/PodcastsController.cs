using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Features.Podcasts.Commands.CreatePodcast;
using PodcastAPI.Application.Features.Podcasts.Queries.GetAllPodcasts;
using PodcastAPI.Application.Features.Podcasts.Queries.GetPodcastById;

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
        public async Task<IActionResult> GetAllPodcasts([FromQuery] GetAllPodcastsQueryRequest getAllPodcastsQueryRequest)
        {
            List<GetAllPodcastsQueryResponse> response = await _mediator.Send(getAllPodcastsQueryRequest);
            return Ok(response);
        }
        
        [HttpGet("{id}")]
        public async Task<IActionResult> GetPodcastById(Guid id)
        {
            var request = new GetPodcastByIdQueryRequest(id);

            GetPodcastByIdQueryResponse response = await _mediator.Send(request);

            if (response == null) return NotFound();
            
            return Ok(response);
        }
        

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> CreatePodcast([FromBody] CreatePodcastCommandRequest createPodcastCommandRequest)
        {
            CreatePodcastCommandResponse response = await _mediator.Send(createPodcastCommandRequest);
            return Ok(response);
        }
    }
}
