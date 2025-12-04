using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.Features.Subscriptions.Commands;
using PodcastAPI.Application.Features.Subscriptions.Queries;
using System.Security.Claims;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubscriptionsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("{podcastId}")]
        public async Task<IActionResult> ToggleSubscription(Guid podcastId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Assuming user ID is stored in the NameIdentifier claim

            if (userIdClaim == null) return Unauthorized();
            
            var command = new ToggleSubscription.Command
            {
                UserId = Guid.Parse(userIdClaim.Value),
                PodcastId = podcastId
            };

            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubscriptions()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return Unauthorized();

            var query = new GetMySubscriptions.Query
            {
                UserId = Guid.Parse(userIdClaim.Value)
            };
            var response = await _mediator.Send(query);
            return Ok(response);
        }
    }
}
