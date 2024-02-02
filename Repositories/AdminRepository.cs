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

    public Task AddAdminAsync(Admin admin)
    {
        try
        {
            _context.Admins.Add(admin);
            return _context.SaveChangesAsync();
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