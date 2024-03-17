namespace BusinessObjects.Dtos.Response;

public class CreateAuctionResponseDto
{
    public Guid AuctionId { get; set; }
    public string Title { get; set; }
    public string Status { get; set; }
    public string Description { get; set; }
    public string RegistrationPeriodStart { get; set; }
    public string RegistrationPeriodEnd { get; set; }
    public decimal InitialPrice { get; set; }
    public string ListingDate { get; set; }
    public string AuctionPeriodStart { get; set; }
    public string AuctionPeriodEnd { get; set; }
    public decimal IncrementalPrice { get; set; }
    public string RealEstateCode { get; set; }
    public string? ThumbnailUrl { get; set; }
}