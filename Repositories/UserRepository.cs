using BusinessObjects.Entities;

namespace Repositories;

public class UserRepository : IUserRepository
{
    private readonly RealEstateDbContext _context;

    public UserRepository()
    {
        _context = new RealEstateDbContext();
    }

    public Task AddAsync(User newUser)
    {
        try
        {
            _context.Add(newUser);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    // public Task<User?> GetOneAsync(string username, string password)
    // {
    //     try
    //     {
    //         var result = _context.Users.SingleOrDefaultAsync(e => e.Username == username && e.Password == password);
    //
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
    //
    // public Task<List<User>> GetAsync()
    // {
    //     try
    //     {
    //         var result = _context.Users.ToListAsync();
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }
}