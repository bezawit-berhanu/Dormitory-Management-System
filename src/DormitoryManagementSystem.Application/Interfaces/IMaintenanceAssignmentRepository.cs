using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IMaintenanceAssignmentRepository
{
    Task<IEnumerable<MaintenanceAssignment>> GetAllAsync();

    Task<MaintenanceAssignment?> GetByIdAsync(int id);

    Task AddAsync(MaintenanceAssignment assignment);

    Task UpdateAsync(MaintenanceAssignment assignment);

    Task DeleteAsync(MaintenanceAssignment assignment);
}