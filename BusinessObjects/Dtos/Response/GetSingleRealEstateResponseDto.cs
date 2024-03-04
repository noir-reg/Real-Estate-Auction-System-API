using BusinessObjects.Entities;

namespace BusinessObjects.Dtos.Response;

public class GetSingleRealEstateResponseDto
{
    public Guid RealEstateId { get; set; }
    public string RealEstateName { get; set; }
    public string Address { get; set; }
    public string Description { get; set; }
    public string ImageUrl { get; set; }
    public string Status { get; set; }
    public IEnumerable<LegalDocument> LegalDocuments { get; set; } = new List<LegalDocument>();
    public Guid OwnerId { get; set; }
    public RealEstateOwner Owner { get; set; }
    public Auction Auction { get; set; }
}