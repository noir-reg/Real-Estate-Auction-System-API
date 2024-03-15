using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Repositories
{
    public class OwnerRepository : IOwnerRepository
    {
        private readonly RealEstateDbContext _context;

        public OwnerRepository()
        {
            _context = new RealEstateDbContext();
        }

        public async Task<RealEstateOwner> AddOwnerAsync(RealEstateOwner owner)
        {
            try
            {
                _context.RealEstateOwners.Add(owner);
                await _context.SaveChangesAsync();
                return owner;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task UpdateOwnerAsync(RealEstateOwner owner)
        {
            try
            {
                _context.RealEstateOwners.Update(owner);
                return _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task DeleteOwnerAsync(RealEstateOwner owner)
        {
            try
            {
                _context.RealEstateOwners.Remove(owner);
                return _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<List<RealEstateOwner>> GetOwners()
        {
            try
            {
                var result = _context.RealEstateOwners.ToListAsync();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public IQueryable<RealEstateOwner> GetOwnerQuery()
        {
            return _context.RealEstateOwners.AsQueryable();
        }

        public Task<RealEstateOwner?> GetOwnerAsync(Expression<Func<RealEstateOwner, bool>> predicate)
        {
            try
            {
                var result = _context.RealEstateOwners.Include(x => x.RealEstates).Select(x => new RealEstateOwner
                {
                    RealEstateOwnerId = x.RealEstateOwnerId,
                    FullName = x.FullName,
                    ContactInformation = x.ContactInformation,
                    CitizenId = x.CitizenId,
                    RealEstates = x.RealEstates.ToList()
                }).SingleOrDefaultAsync(predicate);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Task<int> GetOwnerCountAsync(SearchOwnerQuery? requestSearch)
        {
            try
            {
                var query = _context.RealEstateOwners.AsQueryable();

                if (requestSearch == null) return query.CountAsync();

                if (!string.IsNullOrEmpty(requestSearch.FullName))
                    query = query.Where(x => x.FullName.Contains(requestSearch.FullName));

                return query.CountAsync();
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
