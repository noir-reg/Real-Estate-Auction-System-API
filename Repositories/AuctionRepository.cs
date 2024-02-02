using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
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

    public async Task<ListResponseDto<Auction>> GetAuctions(ListRequestDto request)
    {
        var page = request.Page;
        var pageSize = request.PageSize;
        var offset = request.Offset;
        try
        {
            var data = await _context.Auctions.AsNoTracking().Skip(offset).Take(pageSize).ToListAsync();
            var total = await _context.Auctions.AsNoTracking().CountAsync();

            var result = new ListResponseDto<Auction>()
            {
                Items = data,
                Total = total,
                Page = page,
                PageSize = pageSize
            };

            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task AddAuction(Auction auction)
    {
        try
        {
            _context.Auctions.Add(auction);
            return _context.SaveChangesAsync();
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
}