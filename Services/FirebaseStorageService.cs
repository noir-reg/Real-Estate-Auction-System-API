using Google.Apis.Auth.OAuth2;
using Google.Cloud.Storage.V1;

namespace Services;

public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly StorageClient _storageClient;
    private const string _bucketName = "real-estate-auction-a4998.appspot.com";

    public FirebaseStorageService()
    {
        _storageClient = StorageClient.Create(GoogleCredential.FromFile("D:\\SWD392\\real-estate-auction-a4998-807216049217.json"));
    }

    public async Task<string> UploadFileAsync(string filePath, string objectName, string contentType)
    {
        try
        {
            await using var fileStream = File.OpenRead(filePath);
          
            await _storageClient.UploadObjectAsync(_bucketName, objectName, contentType, fileStream);
            return $"gs://{_bucketName}/{objectName}";
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Stream> DownloadFileAsync(string objectName)
    {
        try
        {
            var stream = new MemoryStream();
            await _storageClient.DownloadObjectAsync(_bucketName, objectName, stream);
            stream.Position = 0;
            return stream;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}