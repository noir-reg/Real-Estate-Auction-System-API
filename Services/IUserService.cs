using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IUserService
{
    Task<List<UserListResponse>> GetUsersAsync(UserQuery request);
}