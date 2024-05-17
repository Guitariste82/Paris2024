namespace Paris2024.Interfaces;

public interface IFileService
{
    void DeleteFile(string fileName);
    Task<string> SaveFile(IFormFile file, string[] allowedExtensions);
}

