using BusinessObjects.Entities;

namespace Repositories;

public interface IAuctionRepository
{
    Task DeleteAuction(Auction auction);
    Task UpdateAuction(Auction auction);

    Task AddAuction(Auction auction);
    // Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto<Auction> request);
}