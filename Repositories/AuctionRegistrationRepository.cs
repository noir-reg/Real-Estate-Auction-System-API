using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;

namespace Repositories;

public class AuctionRegistrationRepository : IAuctionRegistrationRepository
{
    private readonly RealEstateDbContext _context;

    public AuctionRegistrationRepository()
    {
        _context = new RealEstateDbContext();
    }

    public Task AddAuctionRegistrationAsync(AuctionRegistration auctionRegistration)
    {
        try
        {
            _context.AuctionRegistrations.Add(auctionRegistration);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<List<AuctionRegistration>> GetAuctionRegistrations()
    {
        try
        {
            var result = _context.AuctionRegistrations.ToListAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task DeleteAuctionRegistrationAsync(AuctionRegistration auctionRegistration)
    {
        try
        {
            _context.AuctionRegistrations.Remove(auctionRegistration);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task UpdateAuctionRegistrationAsync(AuctionRegistration auctionRegistration)
    {
        try
        {
            _context.AuctionRegistrations.Update(auctionRegistration);
            return _context.SaveChangesAsync();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }

    public Task<AuctionRegistration?> GetAuctionRegistration(Guid id)
    {
        try
        {
            var result = _context.AuctionRegistrations.Where(x => x.RegistrationId
                                                                  == id).SingleOrDefaultAsync();
            return result;
        }
        catch (Exception e)
        {
            throw new Exception(e.Message);
        }
    }
}