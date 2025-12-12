using MediatR;
using Microsoft.AspNetCore.Http;
using PodcastAPI.Application.Abstractions;

namespace PodcastAPI.Application.Features.Files.Commands.UploadFile
{
    public static class UploadFile
    {
        public class Command : IRequest<Response>
        {
            public IFormFile File { get; set; }
            public string FolderName { get; set; } = "images";
        }

        public class Response
        {
            public string FileUrl { get; set; }
        }

        public class Handler : IRequestHandler<Command, Response>
        {
            private readonly IFileService _fileService;

            public Handler(IFileService fileService)
            {
                _fileService = fileService;
            }

            public async Task<Response> Handle(Command request, CancellationToken cancellationToken)
            {
                var filePath = await _fileService.UploadFileAsync(request.File, request.FolderName);

                return new Response 
                { 
                    FileUrl = filePath 
                };
            }
        }
    }
}
