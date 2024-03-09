using System.CodeDom;
using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using BusinessObjects.Enums;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMemberRepository _memberRepository;
    private readonly IAdminRepository _adminRepository;
    private readonly IStaffRepository _staffRepository;

    public UserService(IUserRepository userRepository, IMemberRepository memberRepository,
        IAdminRepository adminRepository, IStaffRepository staffRepository)
    {
        _userRepository = userRepository;
        _memberRepository = memberRepository;
        _adminRepository = adminRepository;
        _staffRepository = staffRepository;
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
            UserId = x.UserId,
            Gender = x.Gender,
            DateOfBirth = x.DateOfBirth.ToString("yyyy-MM-dd"),
            CitizenId = x.CitizenId,
            FirstName = x.FirstName,
            LastName = x.LastName,
            PhoneNumber = x.PhoneNumber
        }).SingleOrDefaultAsync();

        return result;
    }

    public async Task<ResultResponse<DeleteUserResponseDto>?> DeleteUserAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _userRepository.GetUserAsync(x => x.UserId == id);
            if (toBeDeleted == null)
            {
                return new ResultResponse<DeleteUserResponseDto>()
                {
                    Status = Status.NotFound,
                    Messages = new[] { "User not found" }, IsSuccess = false
                };
            }

            await _userRepository.DeleteAsync(toBeDeleted);
            return new ResultResponse<DeleteUserResponseDto>()
            {
                Status = Status.Ok,
                Messages = new[] { "Delete successfully" }, IsSuccess = true,
                Data = new DeleteUserResponseDto()
                {
                    UserId = toBeDeleted.UserId,
                    Username = toBeDeleted.Username,
                    Email = toBeDeleted.Email,
                    Role = toBeDeleted.Role,
                    Gender = toBeDeleted.Gender,
                    DateOfBirth = toBeDeleted.DateOfBirth.ToString("yyyy-MM-dd"),
                    CitizenId = toBeDeleted.CitizenId,
                    FirstName = toBeDeleted.FirstName,
                    LastName = toBeDeleted.LastName,
                    PhoneNumber = toBeDeleted.PhoneNumber
                }
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<DeleteUserResponseDto>()
            {
                Status = Status.Error,
                Messages = new[] { e.Message }, IsSuccess = false
            };
        }
    }

    public async Task<ResultResponse<UpdateUserResponseDto>> UpdateUserAsync(Guid id, UpdateUserRequestDto request)
    {
        try
        {
            var toBeUpdated = await _userRepository.GetUserAsync(x => x.UserId == id);
            if (toBeUpdated == null)
            {
                return new ResultResponse<UpdateUserResponseDto>()
                {
                    Status = Status.NotFound,
                    Messages = new[] { "User not found" },
                    IsSuccess = false
                };
            }
            
            toBeUpdated.Username = !string.IsNullOrEmpty(request.Username) ? request.Username : toBeUpdated.Username;
            toBeUpdated.Email = !string.IsNullOrEmpty(request.Email) ? request.Email : toBeUpdated.Email;
            toBeUpdated.Gender =  request.Gender ?? toBeUpdated.Gender;
            toBeUpdated.DateOfBirth = (DateTime)(request.DateOfBirth != null ? request.DateOfBirth : toBeUpdated.DateOfBirth);
            toBeUpdated.CitizenId = !string.IsNullOrEmpty(request.CitizenId) ? request.CitizenId : toBeUpdated.CitizenId;
            toBeUpdated.FirstName = !string.IsNullOrEmpty(request.FirstName) ? request.FirstName : toBeUpdated.FirstName;
            toBeUpdated.LastName = !string.IsNullOrEmpty(request.LastName) ? request.LastName : toBeUpdated.LastName;
            toBeUpdated.PhoneNumber = !string.IsNullOrEmpty(request.PhoneNumber) ? request.PhoneNumber : toBeUpdated.PhoneNumber;
            
            await _userRepository.Update(toBeUpdated);
            return new ResultResponse<UpdateUserResponseDto>()
            {
                Status = Status.Ok,
                Messages = new[] { "Update successfully" },
                IsSuccess = true,
                Data = new UpdateUserResponseDto()
                {
                    UserId = toBeUpdated.UserId,
                    Username = toBeUpdated.Username,
                    Email = toBeUpdated.Email,
                    Role = toBeUpdated.Role,
                    Gender = toBeUpdated.Gender,
                    DateOfBirth = toBeUpdated.DateOfBirth.ToString("yyyy-MM-dd"),
                    CitizenId = toBeUpdated.CitizenId,
                    FirstName = toBeUpdated.FirstName,
                    LastName = toBeUpdated.LastName,
                    PhoneNumber = toBeUpdated.PhoneNumber
                    
                }
            };
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ResultResponse<CreateUserResponseDto>> CreateUserAsync(CreateUserRequestDto request)
    {
        try
        {
            User? existed = await _userRepository.GetUserAsync(x =>
                x.Username == request.Username || x.Email == request.Email || x.CitizenId == request.CitizenId ||
                x.PhoneNumber == request.PhoneNumber);
            if (existed != null)
            {
                return new ResultResponse<CreateUserResponseDto>()
                {
                    Status = Status.Duplicate,
                    Messages = new[] { "User already existed" },
                    IsSuccess = false
                };
            }

            User data = await CreateUserBasedOnRoleAsync(request);

            return new ResultResponse<CreateUserResponseDto>()
            {
                Status = Status.Ok,
                Messages = new[] { "Create successfully" },
                IsSuccess = true,
                Data = MapUserDataToResponseDto(data)
            };
        }
        catch (Exception e)
        {
            return new ResultResponse<CreateUserResponseDto>()
            {
                Status = Status.Error,
                Messages = new[] { e.Message, e.InnerException?.Message },
                IsSuccess = false
            };
        }
    }

    private async Task<User> CreateUserBasedOnRoleAsync(CreateUserRequestDto request)
    {
        User data = request.Role switch
        {
            nameof(Role.Member) => await _memberRepository.AddMemberAsync(CreateMemberFromRequest(request)),
            nameof(Role.Admin) => await _adminRepository.AddAdminAsync(CreateAdminFromRequest(request)),
            nameof(Role.Staff) => await _staffRepository.AddStaffAsync(CreateStaffFromRequest(request)),
            _ => throw new ArgumentException("Role not found")
        };
        return data;
    }

    private Member CreateMemberFromRequest(CreateUserRequestDto request)
    {
        return new Member
        {
            Email = request.Email,
            Username = request.Username,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            CitizenId = request.CitizenId,
            Password = request.Password,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            IsVerified = true
        };
    }

    private Admin CreateAdminFromRequest(CreateUserRequestDto request)
    {
        return new Admin
        {
            Email = request.Email,
            Username = request.Username,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            CitizenId = request.CitizenId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password
        };
    }

    private Staff CreateStaffFromRequest(CreateUserRequestDto request)
    {
        return new Staff
        {
            Email = request.Email,
            Username = request.Username,
            Gender = request.Gender,
            DateOfBirth = request.DateOfBirth,
            CitizenId = request.CitizenId,
            FirstName = request.FirstName,
            LastName = request.LastName,
            PhoneNumber = request.PhoneNumber,
            Password = request.Password
        };
    }

    private CreateUserResponseDto MapUserDataToResponseDto(User data)
    {
        return new CreateUserResponseDto
        {
            UserId = data.UserId,
            Username = data.Username,
            Email = data.Email,
            Role = data.Role,
            Gender = data.Gender,
            DateOfBirth = data.DateOfBirth.ToString("yyyy-MM-dd"),
            CitizenId = data.CitizenId,
            FirstName = data.FirstName,
            LastName = data.LastName,
            PhoneNumber = data.PhoneNumber
        };
    }
}