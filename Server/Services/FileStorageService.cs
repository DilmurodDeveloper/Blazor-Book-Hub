using BlazorBookHub.Server.Interfaces;

namespace BlazorBookHub.Server.Services
{
    public class FileStorageService : IFileStorageService
    {
        private readonly IWebHostEnvironment _env;

        public FileStorageService(IWebHostEnvironment env)
        {
            _env = env;
        }

        public async Task<string> SaveFileAsync(IFormFile file, string folderName)
        {
            if (file == null || file.Length == 0)
                throw new ArgumentException("File is empty");

            string uploadsFolder = Path.Combine(_env.WebRootPath, "uploads", folderName);

            if (!Directory.Exists(uploadsFolder))
                Directory.CreateDirectory(uploadsFolder);

            string uniqueFileName = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
            string filePath = Path.Combine(uploadsFolder, uniqueFileName);

            try
            {
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(stream);
                }
            }
            catch (Exception ex)
            {
                throw new IOException("File upload failed", ex);
            }

            return Path.Combine("uploads", folderName, uniqueFileName).Replace("\\", "/");
        }

        public async Task DeleteFileAsync(string filePath)
        {
            if (string.IsNullOrWhiteSpace(filePath))
                return;

            string fullPath = Path.Combine(_env.WebRootPath, filePath);

            if (File.Exists(fullPath))
            {
                try
                {
                    await Task.Run(() => File.Delete(fullPath));
                }
                catch (Exception ex)
                {
                    throw new IOException("File deletion failed", ex);
                }
            }
        }
    }
}
