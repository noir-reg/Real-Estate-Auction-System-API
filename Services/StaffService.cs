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
            Email = x.Email,
            DateOfBirth = x.DateOfBirth.ToString("yyyy-MM-dd"),
            Gender = x.Gender,
            Role = x.Role
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

    public async Task<ResultResponse<AddStaffResponseDto>> AddStaffAsync(AddStaffRequestDto request)
    {
        try
        {
            var staff = await _staffRepository.GetStaffAsync(x =>
                x.Email == request.Email || x.CitizenId == request.CitizenId || x.PhoneNumber == request.PhoneNumber ||
                x.Username == request.Username);

            if (staff != null)
            {
                var duplicatedResponse = new ResultResponse<AddStaffResponseDto>
                {
                    IsSuccess = false,
                    Messages = new[] { "Staff already exists" },
                    Status = Status.Duplicate
                };
                return duplicatedResponse;
            }


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


            await _staffRepository.AddStaffAsync(toBeAdded);

            var addedStaff = await _staffRepository.GetStaffAsync(x => x.Email == request.Email);

            var data = new AddStaffResponseDto()
            {
                StaffId = addedStaff.UserId,
                Username = addedStaff.Username,
                Email = addedStaff.Email,
                FirstName = addedStaff.FirstName,
                LastName = addedStaff.LastName,
                PhoneNumber = addedStaff.PhoneNumber,
                DateOfBirth = addedStaff.DateOfBirth,
                Gender = addedStaff.Gender,
                CitizenId = addedStaff.CitizenId
            };

            var successResponse = new ResultResponse<AddStaffResponseDto>
            {
                IsSuccess = true,
                Messages = new[] { "Staff added successfully" },
                Status = Status.Ok,
                Data = data
            };
            return successResponse;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public async Task<ResultResponse<UpdateStaffResponseDto>> UpdateStaffAsync(Guid id, UpdateStaffRequestDto request)
    {
        var toBeUpdated = await _staffRepository.GetStaffAsync(x => x.UserId == id);

        if (toBeUpdated is null)
        {
            var failedResult = new ResultResponse<UpdateStaffResponseDto>
            {
                IsSuccess = false,
                Messages = new[] { "Staff not found" },
                Status = Status.NotFound
            };
            return failedResult;
        }


        toBeUpdated.Username = request.Username ?? toBeUpdated.Username;
        toBeUpdated.Email = request.Email ?? toBeUpdated.Email;
        toBeUpdated.CitizenId = request.CitizenId ?? toBeUpdated.CitizenId;
        toBeUpdated.DateOfBirth = request.DateOfBirth ?? toBeUpdated.DateOfBirth;
        toBeUpdated.Gender = request.Gender ?? toBeUpdated.Gender;
        toBeUpdated.PhoneNumber = request.PhoneNumber ?? toBeUpdated.PhoneNumber;
        toBeUpdated.FirstName = request.FirstName ?? toBeUpdated.FirstName;
        toBeUpdated.LastName = request.LastName ?? toBeUpdated.LastName;

        await _staffRepository.UpdateStaffAsync(toBeUpdated);

        var data = new UpdateStaffResponseDto
        {
            StaffId = toBeUpdated.UserId,
            Username = toBeUpdated.Username,
            Email = toBeUpdated.Email,
            FirstName = toBeUpdated.FirstName,
            LastName = toBeUpdated.LastName,
            PhoneNumber = toBeUpdated.PhoneNumber,
            DateOfBirth = toBeUpdated.DateOfBirth,
            Gender = toBeUpdated.Gender,
            CitizenId = toBeUpdated.CitizenId
        };

        var successResult = new ResultResponse<UpdateStaffResponseDto>
        {
            IsSuccess = true,
            Messages = new[] { "Staff updated successfully" },
            Status = Status.Ok,
            Data = data
        };
        return successResult;
    }

    public async Task<ResultResponse<StaffDetailResponseDto>> GetStaffAsync(Guid id)
    {
        try
        {
            var staff = await _staffRepository.GetStaffAsync(x => x.UserId == id);

            if (staff == null)
            {
                var failedResult = new ResultResponse<StaffDetailResponseDto>
                {
                    IsSuccess = false,
                    Messages = new[] { "Staff not found" },
                    Status = Status.NotFound
                };
                return failedResult;
            }

            var data = new StaffDetailResponseDto
                {
                    UserId = staff.UserId,
                    Username = staff.Username,
                    Email = staff.Email,
                    FirstName = staff.FirstName,
                    LastName = staff.LastName,
                    PhoneNumber = staff.PhoneNumber,
                    DateOfBirth = staff.DateOfBirth,
                    Gender = staff.Gender,
                    CitizenId = staff.CitizenId
                }
                ;


            var successResult = new ResultResponse<StaffDetailResponseDto>
            {
                IsSuccess = true,
                Messages = new[] { "Staff found successfully" },
                Status = Status.Ok,
                Data = data
            };
            return successResult;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
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