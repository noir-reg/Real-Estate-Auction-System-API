using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Services;

public interface IStaffService
{
    Task<ListResponseBaseDto<StaffListResponseDto>> GetStaffsAsync(StaffQuery request);
    Task<ResultResponse<AddStaffResponseDto>> AddStaffAsync(AddStaffRequestDto request);
    Task<ResultResponse<UpdateStaffResponseDto>> UpdateStaffAsync(Guid id, UpdateStaffRequestDto request);
    Task<ResultResponse<StaffDetailResponseDto>> GetStaffAsync(Guid id);
    Task<ResultResponse<DeleteStaffResponseDto>> DeleteStaffAsync(Guid id);
}