using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using Microsoft.AspNetCore.Http;

namespace Services;

public interface IAuctionMediaService
{
    Task<ResultResponse<UploadMediaResponseDto>> UploadMedia(Guid auctionId, IFormFile file);
    Task<ListResponseBaseDto<GetAuctionMediasResponseDto>> GetMedia(Guid auctionId, AuctionMediaQuery query);
}