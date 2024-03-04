namespace Services;

public interface IFirebaseStorageService
{
    Task<Stream> DownloadFileAsync(string objectName);
    Task<string> UploadFileAsync(string filePath, string objectName, string contentType);
}