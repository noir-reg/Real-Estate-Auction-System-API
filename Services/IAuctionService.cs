using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Services;

public interface IAuctionService
{
    Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto dto);
}