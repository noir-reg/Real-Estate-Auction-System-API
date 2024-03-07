namespace BusinessObjects.Dtos.Request;

public class CreateRealEstateResponseDto
{
    public string RealEstateName { get; set; }
    public string Address { get; set; }
    public string Status { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
    public Guid RealEstateId { get; set; }
    public Guid OwnerId { get; set; }
}