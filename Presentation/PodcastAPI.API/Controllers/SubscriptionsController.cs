using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Abstractions;
using System.Security.Claims;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubscriptionsController : ControllerBase
    {
        private readonly ISubscriptionService _subscriptionService;

        public SubscriptionsController(ISubscriptionService subscriptionService)
        {
            _subscriptionService = subscriptionService;
        }

        [HttpPost("{podcastId}")]
        public async Task<IActionResult> ToggleSubscription(Guid podcastId)
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier); // Assuming user ID is stored in the NameIdentifier claim

            if (userIdClaim == null) return Unauthorized();
            
            var userId = Guid.Parse(userIdClaim.Value);

            bool isSubscribed = await _subscriptionService.ToogleSubscriptionAsync(userId, podcastId);

            return Ok(new 
            { 
                Subscribed = isSubscribed,
                Message = isSubscribed ? "Added to favorites." : "Removed from favorites."
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserSubscriptions()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier);

            if (userIdClaim == null) return Unauthorized();
            var userId = Guid.Parse(userIdClaim.Value);
            
            var subscriptions = await _subscriptionService.GetUserSubscriptionsAsync(userId);
            
            return Ok(subscriptions);
        }
    }
}
