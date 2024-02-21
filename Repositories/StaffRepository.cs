using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly RealEstateDbContext _context;

    public StaffRepository()
    {
        _context = new RealEstateDbContext();
    }

    public Task AddStaffAsync(Staff staff)
    {
        try
        {
            _context.Staffs.Add(staff);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateStaffAsync(Staff staff)
    {
        try
        {
            _context.Staffs.Update(staff);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteStaffAsync(Staff staff)
    {
        try
        {
            _context.Staffs.Remove(staff);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Staff>> GetStaffs()
    {
        try
        {
            var result = _context.Staffs.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public IQueryable<Staff> GetStaffQuery()
    {
        return _context.Staffs.AsQueryable();
    }

    public Task<Staff?> GetStaffAsync(Expression<Func<Staff, bool>> predicate)
    {
        try
        {
            var result = _context.Staffs.SingleOrDefaultAsync(predicate);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<int> GetStaffCountAsync(SearchStaffQuery? requestSearch)
    {
        try
        {
            var query = _context.Staffs.AsQueryable();
            
            if(requestSearch == null) return query.CountAsync();

            if (!string.IsNullOrEmpty(requestSearch.Username))
            {
                query = query.Where(x => x.Username.Contains(requestSearch.Username));
            }
            
            return query.CountAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}