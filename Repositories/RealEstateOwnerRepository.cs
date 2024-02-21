using BusinessObjects.Entities;

namespace Repositories;

public class RealEstateOwnerRepository : IRealEstateOwnerRepository
{
    private readonly RealEstateDbContext _context;

    public RealEstateOwnerRepository()
    {
        _context = new RealEstateDbContext();
    }

    public IQueryable<RealEstateOwner> GetRealEstateOwnerQuery()
    {
        return _context.RealEstateOwners.AsQueryable();
    }
}