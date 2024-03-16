namespace BusinessObjects.Dtos.Response;

public class UploadDocumentsResponseDto
{
    public Guid LegalDocumentId { get; set; }
    public string DocumentUrl { get; set; }
    public string DocumentType { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid AuctionId { get; set; }
}