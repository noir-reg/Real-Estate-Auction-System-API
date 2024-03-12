using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IAuctionService
{
    // Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto<Auction> dto);
    Task<ResultResponse<CreateAuctionResponseDto>> CreateAuction(CreateAuctionRequestDto request);
}