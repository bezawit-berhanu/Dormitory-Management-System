using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IMaintenanceRepository
{
    Task<IEnumerable<MaintenanceRequest>> GetAllAsync();

    Task<MaintenanceRequest?> GetByIdAsync(int id);

    Task<MaintenanceRequest> AddAsync(MaintenanceRequest request);

    Task UpdateAsync(MaintenanceRequest request);

    Task<bool> DeleteAsync(int id);
}