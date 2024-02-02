using BusinessObjects.Dtos.Response;

namespace Services;

public interface IAuthService
{
    Task<UserInfo?> Login(string username, string password);
}