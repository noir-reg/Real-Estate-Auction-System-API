using Google.Apis.Auth.OAuth2;
using Google.Apis.Storage.v1.Data;
using Google.Cloud.Storage.V1;
using Microsoft.Extensions.Configuration;

namespace Services;

public class FirebaseStorageService : IFirebaseStorageService
{
    private readonly StorageClient _storageClient;
    private const string _bucketName = "real-estate-auction-a4998.appspot.com";
    private readonly IConfiguration _configuration;

    public FirebaseStorageService(IConfiguration configuration)
    {
        _configuration = configuration;
        _storageClient = StorageClient.Create(credential: GoogleCredential.FromFile(_configuration["GOOGLE_APPLICATION_CREDENTIALS"]));
    }

    public async Task<string> UploadFileAsync(string filePath, string objectName, string contentType)
    {
        try
        {
            await using var fileStream = File.OpenRead(filePath);

            await _storageClient.UploadObjectAsync(_bucketName, objectName, contentType, fileStream);

            var objectAcl = new ObjectAccessControl
            {
                Role = "READER",
                Entity = "allUsers"
            };

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

    public async Task<string> GetDownloadUrlAsync(string objectName)
    {
        var objectInfo = await _storageClient.GetObjectAsync(_bucketName, objectName);
        await _storageClient.UpdateObjectAsync(objectInfo, options: new UpdateObjectOptions()
        {
            PredefinedAcl = PredefinedObjectAcl.PublicRead
        });
        return objectInfo.MediaLink;
    }
}