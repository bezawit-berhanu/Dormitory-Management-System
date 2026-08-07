using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IMaintenanceActivityService
{
    Task<IEnumerable<MaintenanceActivityDto>> GetAllActivitiesAsync();

    Task<MaintenanceActivityDto?> GetActivityByIdAsync(int id);

    Task<MaintenanceActivityDto> CreateActivityAsync(MaintenanceActivityDto dto);

    Task<bool> UpdateActivityAsync(int id, MaintenanceActivityDto dto);

    Task<bool> DeleteActivityAsync(int id);
}