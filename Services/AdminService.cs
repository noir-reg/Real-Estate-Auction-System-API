using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class AdminService : IAdminService
{
    private readonly IAdminRepository _adminRepository;

    public AdminService(IAdminRepository adminRepository)
    {
        _adminRepository = adminRepository;
    }

    public async Task<ListResponseBaseDto<AdminListResponseDto>> GetAdminsAsync(AdminQuery request)
    {
        try
        {
            var query = _adminRepository.GetAdminQuery();

            var offset = request.Offset;
            var pageSize = request.PageSize;
            var page = request.Page;


            if (!string.IsNullOrEmpty(request.Search?.Username))
                query = query.Where(x => x.Username.Contains(request.Search.Username));

            query = request.SortBy switch
            {
                AdminSortBy.Username => request.OrderDirection == OrderDirection.ASC
                    ? query.OrderBy(x => x.Username)
                    : query.OrderByDescending(x => x.Username),
                _ => throw new ArgumentOutOfRangeException()
            };

            query = query.Skip(offset).Take(pageSize);

            var data = await query.Select(x => new AdminListResponseDto
            {
                UserId = x.UserId,
                Username = x.Username,
                Email = x.Email,
                CitizenId = x.CitizenId,
                DateOfBirth = x.DateOfBirth.Date.ToString("yyyy-MM-d"),
                Gender = x.Gender,
                PhoneNumber = x.PhoneNumber,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Role = x.Role
            }).ToListAsync();

            var count = await _adminRepository.GetAdminCountAsync(request.Search);

            var result = new ListResponseBaseDto<AdminListResponseDto>
            {
                Data = data,
                Total = count,
                Page = page,
                PageSize = pageSize
            };

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ResultResponse<CreateAdminResponseDto>> AddAdminAsync(AddAdminRequestDto request)
    {
        try
        {
            var existingAdmin = await _adminRepository.GetAdminAsync(x =>
                x.Email == request.Email || x.CitizenId == request.CitizenId || x.PhoneNumber == request.PhoneNumber);

            if (existingAdmin != null)
                return new ResultResponse<CreateAdminResponseDto>()
                {
                    IsSuccess = false,
                    Messages = new[] { "Admin already exists" },
                    Status = Status.Duplicate
                };

            var admin = new Admin
            {
                Username = request.Username,
                Email = request.Email,
                CitizenId = request.CitizenId,
                DateOfBirth = request.DateOfBirth,
                Gender = request.Gender,
                PhoneNumber = request.PhoneNumber,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Password = request.Password
            };

            var data = await _adminRepository.AddAdminAsync(admin);

            return new ResultResponse<CreateAdminResponseDto>()
            {
                IsSuccess = true,
                Messages = new[] { "Add successfully" },
                Status = Status.Ok,
                Data = new CreateAdminResponseDto
                {
                    UserId = data.UserId,
                    Username = data.Username,
                    Email = data.Email,
                    FirstName = data.FirstName,
                    LastName = data.LastName,
                    PhoneNumber = data.PhoneNumber,
                    DateOfBirth = data.DateOfBirth.Date.ToString("yyyy-MM-d"),
                    Gender = data.Gender,
                    CitizenId = data.CitizenId,
                    Role = data.Role
                }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<CreateAdminResponseDto>(e);
        }
    }

    public async Task<ResultResponse<UpdateAdminResponseDto>> UpdateAdminAsync(Guid id, UpdateAdminRequestDto request)
    {
        try
        {
            var toBeUpdated = await _adminRepository.GetAdminAsync(x => x.UserId == id);

            if (toBeUpdated == null)
                return ErrorResponse.CreateErrorResponse<UpdateAdminResponseDto>(status: Status.NotFound,
                    message: "Admin not found");


            toBeUpdated.Username = !string.IsNullOrEmpty(request.Username) ? request.Username : toBeUpdated.Username;
            toBeUpdated.FirstName =
                !string.IsNullOrEmpty(request.FirstName) ? request.FirstName : toBeUpdated.FirstName;
            toBeUpdated.LastName = !string.IsNullOrEmpty(request.LastName) ? request.LastName : toBeUpdated.LastName;
            toBeUpdated.CitizenId =
                !string.IsNullOrEmpty(request.CitizenId) ? request.CitizenId : toBeUpdated.CitizenId;
            toBeUpdated.DateOfBirth = request.DateOfBirth ?? toBeUpdated.DateOfBirth;
            toBeUpdated.Email = !string.IsNullOrEmpty(request.Email) ? request.Email : toBeUpdated.Email;
            toBeUpdated.PhoneNumber = !string.IsNullOrEmpty(request.PhoneNumber)
                ? request.PhoneNumber
                : toBeUpdated.PhoneNumber;
            toBeUpdated.Gender = request.Gender ?? toBeUpdated.Gender;

            await _adminRepository.UpdateAdminAsync(toBeUpdated);

            return new ResultResponse<UpdateAdminResponseDto>()
            {
                IsSuccess = true,
                Messages = new[] { "Update successfully" },
                Status = Status.Ok,
                Data = new UpdateAdminResponseDto
                {
                    UserId = toBeUpdated.UserId,
                    Username = toBeUpdated.Username,
                    Email = toBeUpdated.Email,
                    FirstName = toBeUpdated.FirstName,
                    LastName = toBeUpdated.LastName,
                    PhoneNumber = toBeUpdated.PhoneNumber,
                    DateOfBirth = toBeUpdated.DateOfBirth.Date.ToString("yyyy-MM-d"),
                    Gender = toBeUpdated.Gender,
                    CitizenId = toBeUpdated.CitizenId,
                    Role = toBeUpdated.Role
                }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<UpdateAdminResponseDto>(e);
        }
    }

    public async Task<ResultResponse<DeleteAdminResponseDto>> DeleteAdminAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _adminRepository.GetAdminAsync(x => x.UserId == id);
            if (toBeDeleted == null)
             return ErrorResponse.CreateErrorResponse<DeleteAdminResponseDto>(status:Status.NotFound,message:"Admin not found"); 

            await _adminRepository.DeleteAdminAsync(toBeDeleted);

            return new ResultResponse<DeleteAdminResponseDto>()
            {
                IsSuccess = true,
                Messages = new[] { "Delete successfully" },
                Status = Status.Ok,
                Data = new DeleteAdminResponseDto
                {
                    UserId = toBeDeleted.UserId,
                    Username = toBeDeleted.Username,
                    Email = toBeDeleted.Email,
                    FirstName = toBeDeleted.FirstName,
                    LastName = toBeDeleted.LastName,
                    PhoneNumber = toBeDeleted.PhoneNumber,
                    DateOfBirth = toBeDeleted.DateOfBirth.Date.ToString("yyyy-MM-d"),
                    Gender = toBeDeleted.Gender,
                    CitizenId = toBeDeleted.CitizenId,
                    Role = toBeDeleted.Role
                }
            };
        }
        catch (Exception e)
        {
            return ErrorResponse.CreateErrorResponse<DeleteAdminResponseDto>(e);
        }
    }
}