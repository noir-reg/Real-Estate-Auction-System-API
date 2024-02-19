using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
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
            var member = new Member
            {
                Email = dto.Email,
                Username = dto.Username,
                Password = dto.Password,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Gender = dto.Gender,
                DateOfBirth = dto.DateOfBirth,
                CitizenId = dto.CitizenId,
                PhoneNumber = dto.PhoneNumber
            };
            var result = _memberRepository.GetMemberAsync(dto.Username, dto.Password);
            if (result.Result != null) throw new Exception("User already exists");
            return _memberRepository.AddMemberAsync(member);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}