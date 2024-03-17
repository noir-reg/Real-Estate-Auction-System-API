using System.Linq.Expressions;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Repositories;

public interface IAuctionRepository
{
    Task DeleteAuction(Auction auction);
    Task UpdateAuction(Auction auction);

    Task<Auction> AddAuction(Auction auction);

    // Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto<Auction> request);
    IQueryable<Auction> GetAuctionQuery();
    Task<int> GetCountAsync(Expression<Func<Auction, bool>>? wherePredicate = null);
    Task<Auction?> GetAuction(Expression<Func<Auction,bool>> predicate);
}