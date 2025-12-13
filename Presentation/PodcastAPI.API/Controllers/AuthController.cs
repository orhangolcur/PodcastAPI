using MediatR;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Features.Auth.Commands.ForgotPassword;
using PodcastAPI.Application.Features.Auth.Commands.Login;
using PodcastAPI.Application.Features.Auth.Commands.RefreshToken;
using PodcastAPI.Application.Features.Auth.Commands.Register;
using PodcastAPI.Application.Features.Auth.Commands.ResetPassword;

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

        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshToken([FromBody] RefreshToken.Command command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success)
            {
                return Unauthorized(response.Message);
            }
            return Ok(response);
        }

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword([FromBody] ForgotPassword.Command command)
        {
            var response = await _mediator.Send(command);
            return Ok(response);
        }

        [HttpPost("reset-password")]
        public async Task<IActionResult> ResetPassword([FromBody] ResetPassword.Command command)
        {
            var response = await _mediator.Send(command);
            if (!response.Success) return BadRequest(response.Message);
            return Ok(response);
        }
    }
}
