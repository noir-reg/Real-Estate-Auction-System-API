using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;

namespace Repositories;

public interface IAdminRepository
{
    IQueryable<Admin> GetAdminQuery();
    Task<int> GetAdminCountAsync(SearchAdminQuery? requestSearch);
    Task UpdateAdminAsync(Admin admin);
    Task DeleteAdminAsync(Admin admin);
    Task<Admin> AddAdminAsync(Admin admin);
    Task<Admin?> GetAdminAsync(Expression<Func<Admin, bool>> predicate);
}