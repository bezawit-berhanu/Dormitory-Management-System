using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface IStaffRepository
{
    Task<IEnumerable<Staff>> GetAllAsync();

    Task<Staff?> GetByIdAsync(int id);

    Task<Staff?> GetByEmployeeIdAsync(string employeeId);
    Task<Staff?> GetByUserIdAsync(int userId);

    Task AddAsync(Staff staff);

    Task UpdateAsync(Staff staff);

    Task DeleteAsync(Staff staff);

    Task<bool> ExistsAsync(int id);

    Task SaveChangesAsync();
}