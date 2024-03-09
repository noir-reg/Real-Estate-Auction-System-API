using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IAdminService
{
    Task<ListResponseBaseDto<AdminListResponseDto>> GetAdminsAsync(AdminQuery request);
    Task<ResultResponse<AddAdminResponseDto>> AddAdminAsync(AddAdminRequestDto request);
    Task<ResultResponse<UpdateAdminResponseDto>> UpdateAdminAsync(Guid id, UpdateAdminRequestDto request);
    Task<ResultResponse<DeleteAdminResponseDto>> DeleteAdminAsync(Guid id);
}