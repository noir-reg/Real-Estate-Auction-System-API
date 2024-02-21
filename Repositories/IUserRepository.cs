using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;

namespace Repositories;

public interface IUserRepository
{
    Task<User?> GetUserAsync(Expression<Func<User, bool>> predicate);
    Task Update(User user);
    Task<List<User>> GetUsersAsync(UserQuery request);
    IQueryable<User> GetUserQuery();
    Task<int> GetUserCount(SearchUserQuery request);
}