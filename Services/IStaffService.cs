using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Services;

public interface IStaffService
{
    Task<ListResponseBaseDto<StaffListResponseDto>> GetStaffsAsync(StaffQuery request);
    Task AddStaffAsync(AddStaffRequestDto request);
    Task UpdateStaffAsync(Guid id, UpdateStaffRequestDto request);
    Task<StaffDetailResponseDto?> GetStaffAsync(Expression<Func<Staff, bool>> predicate);
    Task DeleteStaffAsync(Guid id);
}