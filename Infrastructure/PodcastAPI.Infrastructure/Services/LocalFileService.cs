using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using PodcastAPI.Application.Abstractions;

namespace PodcastAPI.Infrastructure.Services
{
    public class LocalFileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public LocalFileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public async Task<string> UploadFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0) throw new ArgumentException("File is null or empty", nameof(file));
            
            var uploadPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

            if (!Directory.Exists(uploadPath)) Directory.CreateDirectory(uploadPath);
            

            var uniqueFileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";

            var filePath = Path.Combine(uploadPath, uniqueFileName);

            using (var fileStream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(fileStream);
            }

            return $"/{folderName}/{uniqueFileName}";
        }
    }
}
