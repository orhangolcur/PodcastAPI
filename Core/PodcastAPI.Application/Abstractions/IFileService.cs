

using Microsoft.AspNetCore.Http;

namespace PodcastAPI.Application.Abstractions
{
    public interface IFileService
    {
        Task<string> UploadFileAsync(IFormFile file, string folderName);
    }
}
