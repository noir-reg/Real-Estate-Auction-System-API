namespace BusinessObjects.Dtos.Request;

public class CreateAuctionRequestDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public Guid RealEstateId { get; set; }
    public DateTime RegistrationPeriodEnd { get; set; }
    public DateTime RegistrationPeriodStart { get; set; }
    public decimal InitialPrice { get; set; }
    public DateTime AuctionPeriodStart { get; set; }
    public DateTime AuctionPeriodEnd { get; set; }
    public decimal IncrementalPrice { get; set; }
}