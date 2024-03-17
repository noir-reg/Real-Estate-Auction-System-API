namespace BusinessObjects.Dtos.Response;

public class UploadMediaResponseDto
{
    public Guid MediaId { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public string MediaType { get; set; }
    public string Description { get; set; }
}