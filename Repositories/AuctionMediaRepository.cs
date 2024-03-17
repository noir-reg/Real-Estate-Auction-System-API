using System.Linq.Expressions;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class AuctionMediaRepository : IAuctionMediaRepository
{
    private readonly RealEstateDbContext _context;

    public AuctionMediaRepository()
    {
        _context = new RealEstateDbContext();
    }

    public async Task<AuctionMedia> Add(AuctionMedia legalDocument)
    {
        try
        {
            _context.AuctionMedias.Add(legalDocument);
            await _context.SaveChangesAsync();
            return legalDocument;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<AuctionMedia>> GetMedia(Expression<Func<AuctionMedia, bool>>? predicate = null)
    {
        try
        {
            return predicate is null
                ? _context.AuctionMedias.ToListAsync()
                : _context.AuctionMedias.Where(predicate).ToListAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<int> Count(Expression<Func<AuctionMedia, bool>>? predicate = null)
    {
        try
        {
            return predicate is null
                ? _context.AuctionMedias.CountAsync()
                : _context.AuctionMedias.CountAsync(predicate);
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public IQueryable<AuctionMedia> GetQueryable()
    {
        return _context.AuctionMedias.AsQueryable();
    }
}