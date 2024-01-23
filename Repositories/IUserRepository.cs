using BusinessObjects.Entities;

namespace Repositories;

public interface IUserRepository
{
    Task<List<User>> GetAsync();
    Task<User?> GetOneAsync(string username, string password);
    Task AddAsync(User newUser);
}