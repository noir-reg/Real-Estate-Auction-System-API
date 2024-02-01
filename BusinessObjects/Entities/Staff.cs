namespace BusinessObjects.Entities;

public class Staff : User
{
    public IEnumerable<Auction> Auctions { get; set; } = new List<Auction>();
}