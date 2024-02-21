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

    public async Task<UserInfo?> Login(string email, string password)
    {
        try
        {
            var data = await _userRepository.GetUserAsync(x => email == x.Email && password == x.Password);

            if (data == null) throw new Exception("User not found");

            var userInfo = new UserInfo
            {
                UserId = data.UserId,
                Email = data.Email,
                Username = data.Username,
                Role = data.Role
            };
            return userInfo;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task Register(RegisterMemberRequestDto dto)
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

            var email = dto.Email;

            var result = await _memberRepository.GetMemberAsync(x => x.Email == email);
            if (result != null) throw new Exception("User already exists");
            await _memberRepository.AddMemberAsync(member);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}