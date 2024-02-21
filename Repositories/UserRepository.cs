using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
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


    public Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate)
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

    public Task<List<User>> GetUsersAsync(UserQuery request)
    {
        var query = _context.Users.AsQueryable();

        if (!string.IsNullOrEmpty(request.Search?.Username))
            query = query.Where(x => x.Username.Contains(request.Search.Username));

        query = request.SortBy switch
        {
            UserSortBy.Username => request.OrderDirection == OrderDirection.ASC
                ? query.OrderBy(x => x.Username)
                : query.OrderByDescending(x => x.Username)
        };

        query = query.Skip(request.Offset).Take(request.PageSize);

        var data = query.ToListAsync();

        return data;
    }

    public IQueryable<User> GetUserQuery()
    {
        return _context.Users.AsQueryable();
    }

    public Task<int> GetUserCount(SearchUserQuery request)
    {
        var query = _context.Users.AsQueryable();

        if (request == null) return query.CountAsync();

        if (!string.IsNullOrEmpty(request.Username)) query = query.Where(x => x.Username.Contains(request.Username));

        return query.CountAsync();
    }
}