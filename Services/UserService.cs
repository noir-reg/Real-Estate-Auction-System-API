using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<ListResponseBaseDto<UserListResponseDto>> GetUsersAsync(UserQuery request)
    {
        var query = _userRepository.GetUserQuery();

        if (!string.IsNullOrEmpty(request.Search?.Username))
            query = query.Where(x => x.Username.Contains(request.Search.Username));

        query = request.SortBy switch
        {
            UserSortBy.Username => request.OrderDirection == OrderDirection.ASC
                ? query.OrderBy(x => x.Username)
                : query.OrderByDescending(x => x.Username),
            _ => throw new ArgumentOutOfRangeException()
        };

        query = query.Skip(request.Offset).Take(request.PageSize);

        var data = await query.Select(x => new UserListResponseDto
        {
            Username = x.Username,
            Email = x.Email,
            Role = x.Role,
            UserId = x.UserId,
            Gender = x.Gender,
            DateOfBirth = x.DateOfBirth.ToShortDateString(),
            CitizenId = x.CitizenId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber
        }).AsNoTracking().ToListAsync();

        var count = await _userRepository.GetUserCount(request.Search);

        var result = new ListResponseBaseDto<UserListResponseDto>
        {
            Data = data,
            Total = count,
            PageSize = request.PageSize,
            Page = request.Page
        };

        return result;
    }


    public Task<UserDetailResponseDto?> GetUserAsync(Expression<Func<User, bool>> predicate)
    {
        var query = _userRepository.GetUserQuery();

        query = query.Where(predicate);

        var result = query.Select(x => new UserDetailResponseDto
        {
            Email = x.Email,
            Username = x.Username,
            Role = x.Role,
            UserId = x.UserId
        }).SingleOrDefaultAsync();

        return result;
    }

    public Task ChangePasswordAsync(ChangePasswordRequestDto request)
    {
        throw new NotImplementedException();
    }
}