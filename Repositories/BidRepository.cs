﻿using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class BidRepository : IBidRepository
{
    private readonly RealEstateDbContext _context;

    public BidRepository()
    {
        _context = new RealEstateDbContext();
    }

    public IQueryable<Bid> GetBidQuery()
    {
        return _context.Bids.AsQueryable();
    }

    public async Task<Bid> AddBidAsync(Bid bid)
    {
        try
        {
            _context.Bids.Add(bid);
            await _context.SaveChangesAsync();
            return bid;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateBidAsync(Bid bid)
    {
        try
        {
            _context.Bids.Update(bid)
                ;
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteBidAsync(Bid bid)
    {
        try
        {
            _context.Bids.Remove(bid);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<Bid>> GetBids()
    {
        try
        {
            var result = _context.Bids.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
    
    
}