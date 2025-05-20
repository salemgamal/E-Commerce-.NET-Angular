namespace API.Services
{
    public interface IImageService
    {
        Task<List<string>> UploadImagesAsync(List<IFormFile> files, string folderPath);
        void DeleteImageAsync(string imagePath);

    }
}
