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
                DateOfBirth = x.DateOfBirth,
                Gender = x.Gender,
                PhoneNumber = x.PhoneNumber,
                FirstName = x.FirstName,
                LastName = x.LastName
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

    public Task AddAdminAsync(AddAdminRequestDto request)
    {
        try
        {
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

            return _adminRepository.AddAdminAsync(admin);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task UpdateAdminAsync(Guid id, UpdateAdminRequestDto request)
    {
        try
        {
            var toBeUpdated = await _adminRepository.GetAdminAsync(x => x.UserId == id);

            if (toBeUpdated == null) throw new Exception("Admin not found");

            toBeUpdated.Username = request.Username ?? toBeUpdated.Username;
            toBeUpdated.FirstName = request.FirstName ?? toBeUpdated.FirstName;
            toBeUpdated.LastName = request.LastName ?? toBeUpdated.LastName;
            toBeUpdated.CitizenId = request.CitizenId ?? toBeUpdated.CitizenId;
            toBeUpdated.DateOfBirth = request.DateOfBirth ?? toBeUpdated.DateOfBirth;
            toBeUpdated.Email = request.Email ?? toBeUpdated.Email;
            toBeUpdated.PhoneNumber = request.PhoneNumber ?? toBeUpdated.PhoneNumber;

            await _adminRepository.UpdateAdminAsync(toBeUpdated);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task DeleteAdminAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _adminRepository.GetAdminAsync(x => x.UserId == id);
            if (toBeDeleted == null) throw new Exception("Admin not found");

            await _adminRepository.DeleteAdminAsync(toBeDeleted);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}