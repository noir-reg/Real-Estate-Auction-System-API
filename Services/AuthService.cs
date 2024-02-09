using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.Extensions.Configuration;
using Repositories;

namespace Services;

public class AuthService : IAuthService
{
    private readonly IConfiguration _config;
    private readonly IMemberRepository _memberRepository;
    private readonly IUserRepository _userRepository;

    public AuthService(IUserRepository userRepository, IMemberRepository memberRepository, IConfiguration config)
    {
        _userRepository = userRepository;
        _memberRepository = memberRepository;
        _config = config;
    }

    public Task<UserInfo?> Login(string username, string password)
    {
        try
        {
            var userInfo = _userRepository.Login(username, password);
            return userInfo;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task Register(RegisterMemberRequestDto dto)
    {
        try
        {
            return _memberRepository.AddMemberAsync(dto);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}