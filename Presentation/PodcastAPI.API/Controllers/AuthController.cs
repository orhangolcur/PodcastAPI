using MediatR;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Features.Auth.Commands.Login;
using PodcastAPI.Application.Features.Auth.Commands.Register;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IMediator _mediator;

        public AuthController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] Register.Command command)
        {
            try
            {
                var response = await _mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] Login.Command command)
        {
            var response = await _mediator.Send(command);
            if (response == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(response);
        }
    }
}
