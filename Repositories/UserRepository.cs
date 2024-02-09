using System.Linq.Expressions;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly RealEstateDbContext _context;

    public UserRepository()
    {
        _context = new RealEstateDbContext();
    }

    public async Task<UserInfo?> Login(string username, string password)
    {
        try
        {
            var admins = await _context.Admins
                .Where(a => a.Username == username && a.Password == password)
                .Select(a => new UserInfo
                {
                    UserId = a.UserId,
                    Email = a.Email,
                    Username = a.Username,
                    Role = nameof(Admin)
                })
                .ToListAsync();

            var members = await _context.Members
                .Where(m => m.Username == username && m.Password == password)
                .Select(m => new UserInfo
                {
                    UserId = m.UserId,
                    Email = m.Email,
                    Username = m.Username,
                    Role = nameof(Member)
                })
                .ToListAsync();

            var staffs = await _context.Staffs
                .Where(s => s.Username == username && s.Password == password)
                .Select(s => new UserInfo
                {
                    UserId = s.UserId,
                    Email = s.Email,
                    Username = s.Username,
                    Role = nameof(Staff)
                })
                .ToListAsync();

            var users = admins.Concat(members).Concat(staffs);
            return users.SingleOrDefault();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<User?> GetUser(Expression<Func<User, bool>> predicate)
    {
        try
        {
            var result = _context.Users.SingleOrDefaultAsync(predicate);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task Update(User user)
    {
        try
        {
            _context.Users.Update(user);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}