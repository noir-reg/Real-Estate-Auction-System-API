using BusinessObjects.Dtos.Response;
using Microsoft.Extensions.Configuration;
using Repositories;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<UserInfo?> Login(string username, string password)
    {
        try
        {
            var result = await _userRepository.Login(username, password);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}