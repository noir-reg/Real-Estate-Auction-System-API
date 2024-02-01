namespace BusinessObjects.Entities;

public class Admin : User
{
    public Auction Auction { get; set; }
}