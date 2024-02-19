using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Repositories;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public Task<List<UserListResponse>> GetUsersAsync(UserQuery request)
    {
        var data = _userRepository.GetUsersAsync(request);
        return data;
    }
}