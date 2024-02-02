using BusinessObjects.Entities;

namespace Repositories;

public interface IAuctionRegistrationRepository
{
    Task<AuctionRegistration?> GetAuctionRegistration(Guid id);
    Task UpdateAuctionRegistrationAsync(AuctionRegistration auctionRegistration);
    Task DeleteAuctionRegistrationAsync(AuctionRegistration auctionRegistration);
    Task<List<AuctionRegistration>> GetAuctionRegistrations();
    Task AddAuctionRegistrationAsync(AuctionRegistration auctionRegistration);
}