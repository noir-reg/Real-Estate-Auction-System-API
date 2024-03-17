using System.Linq.Expressions;
using BusinessObjects.Entities;

namespace Repositories;

public interface IAuctionMediaRepository
{
    Task<AuctionMedia> Add(AuctionMedia legalDocument);
    Task<List<AuctionMedia>> GetMedia(Expression<Func<AuctionMedia, bool>>? predicate = null);
    Task<int> Count(Expression<Func<AuctionMedia, bool>>? predicate = null);
    IQueryable<AuctionMedia> GetQueryable();
}