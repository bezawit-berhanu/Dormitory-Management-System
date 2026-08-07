using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IMaintenanceAssignmentService
{
    Task<IEnumerable<MaintenanceAssignmentDto>> GetAllAssignmentsAsync();

    Task<MaintenanceAssignmentDto?> GetAssignmentByIdAsync(int id);

    Task<MaintenanceAssignmentDto> CreateAssignmentAsync(MaintenanceAssignmentDto dto);

    Task<bool> UpdateAssignmentAsync(int id, MaintenanceAssignmentDto dto);

    Task<bool> DeleteAssignmentAsync(int id);
}