namespace BusinessObjects.Entities;

public class AuctionRegistration
{
    public Guid RegistrationId { get; set; }
    public Guid MemberId { get; set; }
    public Member Member { get; set; }
    public Guid AuctionId { get; set; }
    public Auction Auction { get; set; }
    public DateTime RegistrationDate { get; set; }
    public string RegistrationStatus { get; set; }
    public decimal DepositAmount { get; set; }
}