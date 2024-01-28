namespace BusinessObjects.Entities;

public class Auction
{
    public Guid AuctionId { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public DateTime ListingDate { get; set; }
    public DateTime PreviewPeriodStart { get; set; }
    public DateTime PreviewPeriodEnd { get; set; }
    public DateTime RegistrationPeriodStart { get; set; }
    public DateTime RegistrationPeriodEnd { get; set; }
    public DateTime AuctionPeriodStart { get; set; }
    public DateTime AuctionPeriodEnd { get; set; }
    public decimal EntryFee { get; set; }
    public decimal InitialPrice { get; set; }
    public decimal IncrementalPrice { get; set; }
    public decimal StartingBid { get; set; }
    public decimal CurrentBid { get; set; }
    public string Status { get; set; }
    public bool IsActive { get; set; }
    public IEnumerable<Bid> Bids { get; set; }
    public Guid StaffId { get; set; }
    public Staff Staff { get; set; }
}