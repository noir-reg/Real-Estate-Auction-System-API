namespace BusinessObjects.Dtos.Request;

public class UpdateAuctionRequestDto
{
    public string? Title { get; set; }
    public string? Description { get; set; }
    public string? RealEstateCode { get; set; }
    public string? Address { get; set; }
    public string? ThumbnailUrl { get; set; }
    public DateTime? RegistrationPeriodStart { get; set; }
    public DateTime? RegistrationPeriodEnd { get; set; }
    public decimal?  InitialPrice { get; set; }
    public DateTime? ListingDate { get; set; }
    public DateTime? AuctionPeriodStart { get; set; }
    public DateTime? AuctionPeriodEnd { get; set; }
    public decimal? IncrementalPrice { get; set; }
    public string? Status { get; set; }
    public Guid? OwnerId { get; set; }
}