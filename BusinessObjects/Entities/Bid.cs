namespace BusinessObjects.Entities;

public class Bid
{
    public Guid BidId { get; set; }
    public decimal Amount { get; set; }
    public DateTime Date { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; }
    public Transaction Transaction { get; set; }
}