using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Services;

public class MaintenanceActivityService : IMaintenanceActivityService
{
    private readonly IMaintenanceActivityRepository _repository;

    public MaintenanceActivityService(IMaintenanceActivityRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MaintenanceActivityDto>> GetAllActivitiesAsync()
    {
        var activities = await _repository.GetAllAsync();

        return activities.Select(a => new MaintenanceActivityDto
        {
            MaintenanceRequestId = a.MaintenanceRequestId,
            PerformedBy = a.PerformedBy,
            ActivityDescription = a.ActivityDescription,
            ActivityDate = a.ActivityDate,
            Status = a.Status
        });
    }

    public async Task<MaintenanceActivityDto?> GetActivityByIdAsync(int id)
    {
        var activity = await _repository.GetByIdAsync(id);

        if (activity == null)
            return null;

        return new MaintenanceActivityDto
        {
            MaintenanceRequestId = activity.MaintenanceRequestId,
            PerformedBy = activity.PerformedBy,
            ActivityDescription = activity.ActivityDescription,
            ActivityDate = activity.ActivityDate,
            Status = activity.Status
        };
    }

    public async Task<MaintenanceActivityDto> CreateActivityAsync(MaintenanceActivityDto dto)
    {
        var activity = new MaintenanceActivity
        {
            MaintenanceRequestId = dto.MaintenanceRequestId,
            PerformedBy = dto.PerformedBy,
            ActivityDescription = dto.ActivityDescription,
            ActivityDate = dto.ActivityDate,
            Status = dto.Status
        };

        await _repository.AddAsync(activity);

        return dto;
    }

    public async Task<bool> UpdateActivityAsync(int id, MaintenanceActivityDto dto)
    {
        var activity = await _repository.GetByIdAsync(id);

        if (activity == null)
            return false;

        activity.MaintenanceRequestId = dto.MaintenanceRequestId;
        activity.PerformedBy = dto.PerformedBy;
        activity.ActivityDescription = dto.ActivityDescription;
        activity.ActivityDate = dto.ActivityDate;
        activity.Status = dto.Status;

        await _repository.UpdateAsync(activity);

        return true;
    }

    public async Task<bool> DeleteActivityAsync(int id)
    {
        var activity = await _repository.GetByIdAsync(id);

        if (activity == null)
            return false;

        await _repository.DeleteAsync(activity);

        return true;
    }
}