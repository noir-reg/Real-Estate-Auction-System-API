using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class AdminRepository : IAdminRepository
{
    private readonly RealEstateDbContext _context;

    public AdminRepository()
    {
        _context = new RealEstateDbContext();
    }

    public IQueryable<Admin> GetAdminQuery()
    {
        return _context.Admins.AsQueryable();
    }

    public Task<int> GetAdminCountAsync(SearchAdminQuery? requestSearch)
    {
        try
        {
            var query = _context.Admins.AsQueryable();
            if (requestSearch == null) return query.CountAsync();

            if (!string.IsNullOrEmpty(requestSearch.Username))
                query = query.Where(x => x.Username.Contains(requestSearch.Username));

            return query.CountAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Admin?> GetAdminAsync(Expression<Func<Admin, bool>> predicate)
    {
        try
        {
            var result = _context.Admins.Where(predicate).FirstOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<Admin> AddAdminAsync(Admin admin)
    {
        try
        {
            _context.Admins.Add(admin);
            await _context.SaveChangesAsync();
            return admin;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteAdminAsync(Admin admin)
    {
        try
        {
            _context.Admins.Remove(admin);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateAdminAsync(Admin admin)
    {
        try
        {
            _context.Admins.Update(admin);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Admin>> GetAdmins()
    {
        try
        {
            var result = _context.Admins.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}