using BusinessObjects.Entities;

namespace Repositories;

public interface IStaffRepository
{
    Task<List<Staff>> GetStaffs();
    Task DeleteStaffAsync(Staff staff);
    Task UpdateStaffAsync(Staff staff);
    Task AddStaffAsync(Staff staff);
}