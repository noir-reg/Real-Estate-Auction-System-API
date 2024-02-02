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

    public Task<UserInfo?> Login(string username, string password)
    {
        try
        {
            var result = _context.Admins.OfType<User>().Concat(_context.Staffs.OfType<User>())
                .Concat(_context.Members.OfType<User>()).Where(x => x.Username == username && x.Password == password)
                .Select(x => new UserInfo
                {
                    UserId = x.UserId,
                    Email = x.Email,
                    Username = x.Username,
                    Role = x.Role
                }).SingleOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}