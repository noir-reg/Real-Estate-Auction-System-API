using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IRealEstateService
{
    Task<ResultResponse<CreateRealEstateResponseDto>> CreateRealEstate(CreateRealEstateRequestDto request);
    Task<ListResponseBaseDto<GetRealEstatesResponseDto>> GetRealEstates(RealEstateQuery request);
    Task<ResultResponse<UpdateRealEstateResponseDto>> UpdateRealEstate(Guid realEstateId,UpdateRealEstateRequestDto request);
    Task<ResultResponse<DeleteRealEstateResponseDto>> DeleteRealEstate(Guid id);
    Task<ResultResponse<GetSingleRealEstateResponseDto>> GetRealEstateById(Guid id);
}