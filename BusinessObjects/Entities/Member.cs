namespace BusinessObjects.Entities;

public class Member : User
{
    public bool IsVerified { get; set; } = false;
    public IEnumerable<AuctionRegistration> AuctionRegistrations { get; set; } = new List<AuctionRegistration>();
    public IEnumerable<Bid> Bids { get; set; } = new List<Bid>();
}