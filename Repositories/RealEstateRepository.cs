using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class RealEstateRepository : IRealEstateRepository
{
    private readonly RealEstateDbContext _context;

    public RealEstateRepository()
    {
        _context = new RealEstateDbContext();
    }

    public Task<List<RealEstate>> GetRealEstates()
    {
        try
        {
            var result = _context.RealEstates.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<RealEstate?> GetRealEstate(Guid realEstateId)
    {
        try
        {
            var result = _context.RealEstates.Where(x => x.RealEstateId == realEstateId).SingleOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task AddRealEstateAsync(RealEstate realEstate)
    {
        try
        {
            _context.RealEstates.Add(realEstate);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateRealEstateAsync(RealEstate realEstate)
    {
        try
        {
            _context.RealEstates.Update(realEstate);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteRealEstateAsync(RealEstate realEstate)
    {
        try
        {
            _context.RealEstates.Remove(realEstate);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}