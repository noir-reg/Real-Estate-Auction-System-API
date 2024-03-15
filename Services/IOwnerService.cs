using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public interface IOwnerService
    {
        Task<ListResponseBaseDto<OwnerResponse>> GetOwnersAsync(OwnerQuery request);
        Task<ResultResponse<AddOwnerResponseDto>> AddOwnerAsync(AddOwnerRequestDto request);
        Task<ResultResponse<OwnerUpdateResponseDto>> UpdateOwnerAsync(Guid id, OwnerUpdateRequestDto request);
        Task<ResultResponse<OwnerResponse>> GetOwnerAsync(Guid id);
        Task<ResultResponse<OwnerDeleteResponse>> DeleteOwnerAsync(Guid id);
        List<RealEstateOwner> GetAllOwners();
    }
}
