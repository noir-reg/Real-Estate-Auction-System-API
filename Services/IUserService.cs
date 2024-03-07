using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Services;

public interface IUserService
{
    Task<ListResponseBaseDto<UserListResponseDto>> GetUsersAsync(UserQuery request);
    Task<UserDetailResponseDto?> GetUserAsync(Expression<Func<User, bool>> predicate);
    Task ChangePasswordAsync(ChangePasswordRequestDto request);
}