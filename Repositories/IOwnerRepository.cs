using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public interface IOwnerRepository
    {
        Task<List<RealEstateOwner>> GetOwners();
        Task DeleteOwnerAsync(RealEstateOwner owner);
        Task UpdateOwnerAsync(RealEstateOwner owner);
        Task<RealEstateOwner> AddOwnerAsync(RealEstateOwner owner);
        IQueryable<RealEstateOwner> GetOwnerQuery();
        Task<RealEstateOwner?> GetOwnerAsync(Expression<Func<RealEstateOwner, bool>> predicate);
        Task<int> GetOwnerCountAsync(SearchOwnerQuery? requestSearch);
    }
}
