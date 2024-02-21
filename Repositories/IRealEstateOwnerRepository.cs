using BusinessObjects.Entities;

namespace Repositories;

public interface IRealEstateOwnerRepository
{
    IQueryable<RealEstateOwner> GetRealEstateOwnerQuery();
}