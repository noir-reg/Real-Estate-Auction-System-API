using BusinessObjects.Entities;

namespace Repositories;

public interface IRealEstateRepository
{
    Task DeleteRealEstateAsync(RealEstate realEstate);
    Task UpdateRealEstateAsync(RealEstate realEstate);
    Task AddRealEstateAsync(RealEstate realEstate);
    Task<RealEstate?> GetRealEstate(Guid realEstateId);
    IQueryable<RealEstate> GetRealEstatesQuery();
}