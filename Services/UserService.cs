using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<bool> Login(string username, string password)
    {
        try
        {
            var result = await _userRepository.GetOneAsync(username, password);
            if (result == null) return false;
            return true;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<User>> GetAll()
    {
        try
        {
            var result = _userRepository.GetAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task AddAsync(User newUser)
    {
        try
        {
            var result = _userRepository.AddAsync(newUser);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}