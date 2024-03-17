namespace BusinessObjects.Entities;

public class Auction
{
    public Guid AuctionId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ListingDate { get; set; }
    public DateTime RegistrationPeriodStart { get; set; }
    public DateTime RegistrationPeriodEnd { get; set; }
    public DateTime AuctionPeriodStart { get; set; }
    public DateTime AuctionPeriodEnd { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal IncrementalPrice { get; set; }
    public Guid? StartingBidId { get; set; }
    public Guid? CurrentBidId { get; set; }
    public Guid? WinningBidId { get; set; }
    public Bid? StartingBid { get; set; }
    public Bid? CurrentBid { get; set; }
    public Bid? WinningBid { get; set; }
    public string Status { get; set; }
    public IEnumerable<Bid> Bids { get; set; } = new List<Bid>();
    public Guid? StaffId { get; set; }
    public Staff? Staff { get; set; }
    public Guid AdminId { get; set; }
    public Admin Admin { get; set; }
    public IEnumerable<AuctionRegistration> AuctionRegistrations { get; set; } = new List<AuctionRegistration>();
    public string RealEstateCode { get; set; }
    public Guid OwnerId { get; set; }
    public RealEstateOwner Owner { get; set; }
    public IEnumerable<LegalDocument>? LegalDocuments { get; set; } = new List<LegalDocument>();
    public IEnumerable<AuctionMedia> AuctionMedias { get; set; } = new List<AuctionMedia>();
    public string? Address { get; set; }
    public string? ThumbnailUrl { get; set; }
}

public static class AuctionStatus
{
    public const string ToBeSold = "Sắp diễn ra";
    public const string Selling = "Đang diễn ra";
    public const string Sold = "Đã thanh lý";
}