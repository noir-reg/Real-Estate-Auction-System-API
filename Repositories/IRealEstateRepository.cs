using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Dtos.Response;
using BusinessObjects.Entities;

namespace Repositories;

public interface IRealEstateRepository
{
    Task DeleteRealEstateAsync(RealEstate realEstate);
    Task<RealEstate> UpdateRealEstateAsync(RealEstate realEstate);
    Task<RealEstate?> GetRealEstate(Expression<Func<RealEstate, bool>> predicate);
    IQueryable<RealEstate> GetRealEstatesQuery();
    Task<RealEstate> AddRealEstateAsync(RealEstate realEstate);
    Task<int> GetCount(SearchRealEstateQuery? request);
}