using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public interface IBidRepository
{
    IQueryable<Bid> GetBidQuery();


    Task<Bid> AddBidAsync(Bid bid);


    Task UpdateBidAsync(Bid bid);


    Task DeleteBidAsync(Bid bid);


    Task<List<Bid>> GetBids();
     
}