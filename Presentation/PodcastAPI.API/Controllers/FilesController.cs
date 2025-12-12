using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PodcastAPI.Application.Features.Files.Commands.UploadFile;

namespace PodcastAPI.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FilesController : ControllerBase
    {
        private readonly IMediator _mediator;

        public FilesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost("upload")]
        public async Task<IActionResult> UploadFile(IFormFile file)
        {
            var command = new UploadFile.Command
            {
                File = file,
                FolderName = "images"
            };

            var response = await _mediator.Send(command);

            var request = HttpContext.Request;
            var baseUrl = $"{request.Scheme}://{request.Host}{request.PathBase}";
            var fullUrl = $"{baseUrl}/{response.FileUrl}";

            return Ok(new { url = fullUrl });
        }   
    }
}
