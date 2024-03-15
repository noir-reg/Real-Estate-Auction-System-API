using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
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

    public Task<RealEstate?> GetRealEstate(Expression<Func<RealEstate, bool>> predicate)
    {
        try
        {
            var result = _context.RealEstates.Include(x=>x.Owner).SingleOrDefaultAsync(predicate);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }


    public async Task<RealEstate> UpdateRealEstateAsync(RealEstate realEstate)
    {
        try
        {
            _context.RealEstates.Update(realEstate);
             await _context.SaveChangesAsync();

             return realEstate;
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

    public IQueryable<RealEstate> GetRealEstatesQuery()
    {
        return _context.RealEstates.AsQueryable();
    }

    public async Task<RealEstate> AddRealEstateAsync(RealEstate realEstate)
    {
        try
        {
            _context.RealEstates.Add(realEstate);
            await _context.SaveChangesAsync();

            return realEstate;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<int> GetCount(SearchRealEstateQuery? request)
    {
        try
        {
            var queryable = _context.RealEstates.AsQueryable();

            if (request == null)
            {
                return queryable.CountAsync();
            }


            queryable = queryable.Where(x => x.RealEstateName.Contains(request.RealEstateName));

            return queryable.CountAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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
}