namespace BusinessObjects.Dtos.Response;

public class DeleteAuctionResponseDto
{
    public Guid OwnerId { get; set; }
    public string Status { get; set; }
    public decimal IncrementalPrice { get; set; }
    public DateTime AuctionPeriodEnd { get; set; }
    public DateTime AuctionPeriodStart { get; set; }
    public DateTime ListingDate { get; set; }
    public decimal InitialPrice { get; set; }
    public DateTime RegistrationPeriodEnd { get; set; }
    public DateTime RegistrationPeriodStart { get; set; }
    public string? ThumbnailUrl { get; set; }
    public string? Address { get; set; }
    public string RealEstateCode { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public Guid AuctionId { get; set; }
}