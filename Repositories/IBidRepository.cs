using BusinessObjects.Entities;

namespace Repositories;

public interface IBidRepository
{
    IQueryable<Bid> GetBidQuery();
}