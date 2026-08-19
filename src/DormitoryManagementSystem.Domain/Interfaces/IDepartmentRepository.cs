using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface IDepartmentRepository
{
    Task<Department?> GetByRegistrarIdAsync(int departmentId);

    Task AddAsync(Department department);

    Task SaveChangesAsync();
}