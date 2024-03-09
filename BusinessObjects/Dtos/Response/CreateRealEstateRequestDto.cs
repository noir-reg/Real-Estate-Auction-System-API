namespace BusinessObjects.Dtos.Response;

public class CreateRealEstateRequestDto
{
    public Guid OwnerId { get; set; }
    public string RealEstateName { get; set; }
    public string Address { get; set; }
    public string ImageUrl { get; set; }
    public string Description { get; set; }
}