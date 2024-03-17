namespace BusinessObjects.Dtos.Response;

public class AuctionPostListResponseDto
{
    public string Title { get; set; }
    public Guid AuctionId { get; set; }
    public DateTime AuctionStart { get; set; }
    public string Thumbnail { get; set; } = "https://data.lvo.vn/media/upload/1004603/online-auction-sales_istock.jpg";
    public decimal InitialPrice { get; set; }
    public string Status { get; set; }
}