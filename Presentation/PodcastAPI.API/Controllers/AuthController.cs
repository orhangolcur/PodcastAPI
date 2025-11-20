using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Abstractions;
using PodcastAPI.Application.DTOs.Auth;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest registerRequest)
        {
            try
            {
                var authResponse = await _authService.RegisterAsync(registerRequest);
                return Ok(authResponse);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest loginRequest)
        {
            var authResponse = await _authService.LoginAsync(loginRequest);
            if (authResponse == null)
            {
                return Unauthorized("Invalid credentials.");
            }
            return Ok(authResponse);
        }
    }
}
