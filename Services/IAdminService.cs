using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IAdminService
{
    Task<ListResponseBaseDto<AdminListResponseDto>> GetAdminsAsync(AdminQuery request);
    Task AddAdminAsync(AddAdminRequestDto request);
    Task UpdateAdminAsync(Guid id, UpdateAdminRequestDto request);
    Task DeleteAdminAsync(Guid id);
}