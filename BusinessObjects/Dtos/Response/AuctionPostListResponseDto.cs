namespace BusinessObjects.Dtos.Response;

public class AuctionPostListResponseDto
{
    public string Title { get; set; }
    public Guid AuctionId { get; set; }
    public DateTime AuctionStart { get; set; }
    public string Thumbnail { get; set; }
    public decimal InitialPrice { get; set; }
}