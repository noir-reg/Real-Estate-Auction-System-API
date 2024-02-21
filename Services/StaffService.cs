using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using Repositories;

namespace Services;

public class StaffService : IStaffService
{
    private readonly IStaffRepository _staffRepository;

    public StaffService(IStaffRepository staffRepository)
    {
        _staffRepository = staffRepository;
    }

    public async Task<ListResponseBaseDto<StaffListResponseDto>> GetStaffsAsync(StaffQuery request)
    {
        var offset = request.Offset;
        var pageSize = request.PageSize;
        var page = request.Page;

        var query = _staffRepository.GetStaffQuery();

        if (!string.IsNullOrEmpty(request.Search?.Username))
            query = query.Where(x => x.Username.Contains(request.Search.Username));

        query = request.SortBy switch
        {
            StaffSortBy.Username => request.OrderDirection == OrderDirection.ASC
                ? query.OrderBy(x => x.Username)
                : query.OrderByDescending(x => x.Username),
            _ => throw new ArgumentOutOfRangeException()
        };

        var data = await query.Select(x => new StaffListResponseDto
        {
            UserId = x.UserId,
            Username = x.Username, FirstName = x.FirstName,
            LastName = x.LastName,
            Email = x.Email
        }).ToListAsync();

        var count = await _staffRepository.GetStaffCountAsync(request.Search);

        var result = new ListResponseBaseDto<StaffListResponseDto>
        {
            Data = data,
            Page = page,
            PageSize = pageSize,
            Total = count
        };

        return result;
    }

    public Task AddStaffAsync(AddStaffRequestDto request)
    {
        var toBeAdded = new Staff
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password,
            CitizenId = request.CitizenId,
            DateOfBirth = request.DateOfBirth,
            Gender = request.Gender,
            PhoneNumber = request.PhoneNumber,
            FirstName = request.FirstName,
            LastName = request.LastName
        };

        return _staffRepository.AddStaffAsync(toBeAdded);
    }

    public async Task UpdateStaffAsync(Guid id, UpdateStaffRequestDto request)
    {
        var toBeUpdated = await _staffRepository.GetStaffAsync(x => x.UserId == id);

        if (toBeUpdated is null) throw new Exception("Staff not found");

        toBeUpdated.Username = request.Username ?? toBeUpdated.Username;
        toBeUpdated.Email = request.Email ?? toBeUpdated.Email;
        toBeUpdated.CitizenId = request.CitizenId ?? toBeUpdated.CitizenId;
        toBeUpdated.DateOfBirth = request.DateOfBirth ?? toBeUpdated.DateOfBirth;
        toBeUpdated.Gender = request.Gender ?? toBeUpdated.Gender;
        toBeUpdated.PhoneNumber = request.PhoneNumber ?? toBeUpdated.PhoneNumber;
        toBeUpdated.FirstName = request.FirstName ?? toBeUpdated.FirstName;
        toBeUpdated.LastName = request.LastName ?? toBeUpdated.LastName;

        await _staffRepository.UpdateStaffAsync(toBeUpdated);
    }

    public Task<StaffDetailResponseDto?> GetStaffAsync(Expression<Func<Staff, bool>> predicate)
    {
        var query = _staffRepository.GetStaffQuery();

        query = query.Where(predicate);

        var result = query.Select(x => new StaffDetailResponseDto
        {
            UserId = x.UserId,
            Username = x.Username,
            Email = x.Email
        }).SingleOrDefaultAsync();

        return result;
    }

    public async Task DeleteStaffAsync(Guid id)
    {
        try
        {
            var toBeDeleted = await _staffRepository.GetStaffAsync(x => x.UserId == id);

            if (toBeDeleted is null) throw new Exception("Staff not found");

            await _staffRepository.DeleteStaffAsync(toBeDeleted);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}