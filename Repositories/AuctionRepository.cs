using System.Linq.Expressions;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class AuctionRepository : IAuctionRepository
{
    private readonly RealEstateDbContext _context;

    public AuctionRepository()
    {
        _context = new RealEstateDbContext();
    }

    // public async Task<Auction> GetAuctions(ListRequestDto<Auction> request)
    // {
    //     var page = request.Page;
    //     var pageSize = request.PageSize;
    //     var offset = request.Offset;
    //     try
    //     {
    //         var data = await _context.Auctions.AsNoTracking().Skip(offset).Take(pageSize).ToListAsync();
    //         var total = await _context.Auctions.AsNoTracking().CountAsync();
    //
    //         var result = new ListResponseDto<Auction>
    //         {
    //             Items = data,
    //             Total = total,
    //             Page = page,
    //             PageSize = pageSize
    //         };
    //
    //         return result;
    //     }
    //     catch (Exception e)
    //     {
    //         throw new Exception(e.Message);
    //     }
    // }

    public async Task<Auction> AddAuction(Auction auction)
    {
        try
        {
            _context.Auctions.Add(auction);
            await _context.SaveChangesAsync();
            return auction;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateAuction(Auction auction)
    {
        try
        {
            _context.Auctions.Update(auction);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteAuction(Auction auction)
    {
        try
        {
            _context.Auctions.Remove(auction);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public IQueryable<Auction> GetAuctionQuery()
    {
        return _context.Auctions.AsQueryable();
    }

    public Task<int> GetCountAsync(Expression<Func<Auction, bool>>? wherePredicate)
    {
        try
        {
            return wherePredicate == null
                ? _context.Auctions.AsNoTracking().CountAsync()
                : _context.Auctions.AsNoTracking().CountAsync(wherePredicate);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<Auction?> GetAuction(Expression<Func<Auction,bool>> predicate)
    {
        return _context.Auctions.SingleOrDefaultAsync(predicate);
    }
}