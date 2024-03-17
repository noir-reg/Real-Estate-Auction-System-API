namespace BusinessObjects.Entities;

public class AuctionMedia
{
    public Guid MediaId { get; set; }
    public string FileName { get; set; }
    public string FileUrl { get; set; }
    public string Description { get; set; }
    public string MediaType { get; set; }
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; }
}