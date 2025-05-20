using API.Services;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Options;

namespace API.Repositories.Service
{
    public class ImageService : IImageService
    {
        private readonly string _imagesBasePath;
        private readonly string _webRootPath;

        public ImageService(IWebHostEnvironment env, IOptions<ImageSettings> imageSettings)
        {
            _imagesBasePath = imageSettings.Value.BasePath ?? "Images";
            _webRootPath = env.WebRootPath ?? throw new ArgumentNullException("WebRootPath is null");
        }

        public void DeleteImageAsync(string imagePath)
        {
            var fullPath = Path.Combine(_webRootPath, imagePath);
            if (File.Exists(fullPath))
            {
                File.Delete(fullPath);
            }
        }

        public async Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderPath)
        {
            if (files == null || folderPath == null)
                throw new ArgumentNullException("Files or folderPath is null");

            var savedImagePaths = new List<string>();
            var uploadDirectory = Path.Combine(_imagesBasePath, folderPath);
            var fullUploadPath = Path.Combine(_webRootPath, uploadDirectory);

            Directory.CreateDirectory(fullUploadPath); // Ensures folder exists

            foreach (var file in files)
            {
                if (file.Length == 0 || !IsImageFile(file))
                    continue;

                var uniqueFileName = $"{Guid.NewGuid()}_{Path.GetFileName(file.FileName)}";
                var relativeImagePath = Path.Combine(uploadDirectory, uniqueFileName).Replace("\\", "/");
                var fullFilePath = Path.Combine(fullUploadPath, uniqueFileName);

                using (var stream = new FileStream(fullFilePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }

                savedImagePaths.Add(relativeImagePath);
            }

            return savedImagePaths;
        }

        private bool IsImageFile(IFormFile file)
        {
            var allowedContentTypes = new[] { "image/jpeg", "image/png", "image/gif" };
            var allowedExtensions = new[] { ".jpg", ".jpeg", ".png", ".gif" };

            return allowedContentTypes.Contains(file.ContentType) &&
                   allowedExtensions.Contains(Path.GetExtension(file.FileName).ToLower());
        }
    }

    public class ImageSettings
    {
        public string BasePath { get; set; }
    }
}
