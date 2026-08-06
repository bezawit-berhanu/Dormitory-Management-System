namespace DormitoryManagementSystem.Application.Interfaces;

using DormitoryManagementSystem.Application.DTOs;

public interface IMaintenanceService
{
    Task<IEnumerable<MaintenanceDto>> GetAllAsync();

    Task<MaintenanceDto?> GetByIdAsync(int id);

    Task<MaintenanceDto> CreateAsync(MaintenanceDto maintenanceDto);

    Task<MaintenanceDto?> UpdateAsync(int id, MaintenanceDto maintenanceDto);

    Task<bool> DeleteAsync(int id);


}