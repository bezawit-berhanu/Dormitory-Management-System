using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Services;

public class MaintenanceAssignmentService : IMaintenanceAssignmentService
{
    private readonly IMaintenanceAssignmentRepository _repository;

    public MaintenanceAssignmentService(IMaintenanceAssignmentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MaintenanceAssignmentDto>> GetAllAssignmentsAsync()
    {
        var assignments = await _repository.GetAllAsync();

        return assignments.Select(a => new MaintenanceAssignmentDto
        {
            MaintenanceRequestId = a.MaintenanceRequestId,
            UserId = a.UserId,
            AssignedDate = a.AssignedDate,
            Status = a.Status
        });
    }

    public async Task<MaintenanceAssignmentDto?> GetAssignmentByIdAsync(int id)
    {
        var assignment = await _repository.GetByIdAsync(id);

        if (assignment == null)
            return null;

        return new MaintenanceAssignmentDto
        {
            MaintenanceRequestId = assignment.MaintenanceRequestId,
            UserId = assignment.UserId,
            AssignedDate = assignment.AssignedDate,
            Status = assignment.Status
        };
    }

    public async Task<MaintenanceAssignmentDto> CreateAssignmentAsync(MaintenanceAssignmentDto dto)
    {
        var assignment = new MaintenanceAssignment
        {
            MaintenanceRequestId = dto.MaintenanceRequestId,
            UserId = dto.UserId,
            AssignedDate = dto.AssignedDate,
            Status = dto.Status
        };

        await _repository.AddAsync(assignment);

        return dto;
    }

    public async Task<bool> UpdateAssignmentAsync(int id, MaintenanceAssignmentDto dto)
    {
        var assignment = await _repository.GetByIdAsync(id);

        if (assignment == null)
            return false;

        assignment.MaintenanceRequestId = dto.MaintenanceRequestId;
        assignment.UserId = dto.UserId;
        assignment.AssignedDate = dto.AssignedDate;
        assignment.Status = dto.Status;

        await _repository.UpdateAsync(assignment);

        return true;
    }

    public async Task<bool> DeleteAssignmentAsync(int id)
    {
        var assignment = await _repository.GetByIdAsync(id);

        if (assignment == null)
            return false;

        await _repository.DeleteAsync(assignment);

        return true;
    }
}