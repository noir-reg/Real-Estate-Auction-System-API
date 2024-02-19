using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Repositories;

public interface IUserRepository
{
    Task<UserInfo?> Login(string email, string password);
    Task<User?> GetUser(Expression<Func<User, bool>> predicate);
    Task Update(User user);
    Task<List<UserListResponse>> GetUsersAsync(UserQuery request);
}