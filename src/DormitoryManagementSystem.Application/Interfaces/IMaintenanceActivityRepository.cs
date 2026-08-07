using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IMaintenanceActivityRepository
{
    Task<IEnumerable<MaintenanceActivity>> GetAllAsync();

    Task<MaintenanceActivity?> GetByIdAsync(int id);

    Task AddAsync(MaintenanceActivity activity);

    Task UpdateAsync(MaintenanceActivity activity);

    Task DeleteAsync(MaintenanceActivity activity);
}