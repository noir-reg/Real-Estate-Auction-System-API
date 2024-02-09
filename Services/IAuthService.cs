using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IAuthService
{
    Task<UserInfo?> Login(string username, string password);
    Task Register(RegisterMemberRequestDto dto);
}