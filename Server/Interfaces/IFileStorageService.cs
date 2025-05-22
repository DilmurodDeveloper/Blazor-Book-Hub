namespace BlazorBookHub.Server.Interfaces
{
    public interface IFileStorageService
    {
        Task<string> SaveFileAsync(IFormFile file, string folderName);
        Task DeleteFileAsync(string filePath);
    }

}
