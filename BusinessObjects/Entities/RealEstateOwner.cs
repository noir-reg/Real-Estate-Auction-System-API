namespace BusinessObjects.Entities;

public class RealEstateOwner
{
    public Guid RealEstateOwnerId { get; set; }
    public string FullName { get; set; }
    public string CitizenId { get; set; }
    public string ContactInformation { get; set; }
    public IEnumerable<RealEstate> RealEstates { get; set; } = new List<RealEstate>();
}