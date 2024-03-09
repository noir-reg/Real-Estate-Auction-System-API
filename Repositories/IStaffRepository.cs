using System.Linq.Expressions;
using BusinessObjects.Dtos.Request;
using BusinessObjects.Entities;

namespace Repositories;

public interface IStaffRepository
{
    Task<List<Staff>> GetStaffs();
    Task DeleteStaffAsync(Staff staff);
    Task UpdateStaffAsync(Staff staff);
    Task<Staff> AddStaffAsync(Staff staff);
    IQueryable<Staff> GetStaffQuery();
    Task<Staff?> GetStaffAsync(Expression<Func<Staff, bool>> predicate);
    Task<int> GetStaffCountAsync(SearchStaffQuery? requestSearch);
}