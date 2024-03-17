using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;

namespace Services;

public interface IAuctionService
{
    // Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto<Auction> dto);
    Task<ResultResponse<CreateAuctionResponseDto>> CreateAuction(CreateAuctionRequestDto request);
    Task<ResultResponse<AuctionPostDetailResponseDto>> GetAuctionById(Guid auctionId);
    Task<ListResponseBaseDto<AuctionPostListResponseDto>> GetAuctions(AuctionQuery request);
}