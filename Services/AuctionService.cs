using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;
using Repositories;

namespace Services;

public class AuctionService : IAuctionService
{
    private readonly IAuctionRepository _auctionRepository;

    public AuctionService(IAuctionRepository auctionRepository)
    {
        _auctionRepository = auctionRepository;
    }

    public Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto dto)
    {
        try
        {
            var result = _auctionRepository.GetAuctions(dto);
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}