using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;


namespace DormitoryManagementSystem.Application.Services;

public class MaintenanceService : IMaintenanceService
{
    private readonly IMaintenanceRepository _repository;

    public MaintenanceService(IMaintenanceRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<MaintenanceDto>> GetAllAsync()
    {
        var requests = await _repository.GetAllAsync();

        return requests.Select(m => new MaintenanceDto
        {
            MaintenanceRequestId = m.MaintenanceRequestId,
            RoomId = m.RoomId,
            RequestedBy = m.RequestedBy,
            Title = m.Title,
            Description = m.Description,
            Priority = m.Priority,
            RequestDate = m.RequestDate,
            Status = m.Status
        });
    }

    public async Task<MaintenanceDto?> GetByIdAsync(int id)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request == null)
            return null;

        return new MaintenanceDto
        {
            MaintenanceRequestId = request.MaintenanceRequestId,
            RoomId = request.RoomId,
            RequestedBy = request.RequestedBy,
            Title = request.Title,
            Description = request.Description,
            Priority = request.Priority,
            RequestDate = request.RequestDate,
            Status = request.Status
        };
    }

    public async Task<MaintenanceDto> CreateAsync(MaintenanceDto maintenanceDto)
    {
        var request = new MaintenanceRequest
        {
            RoomId = maintenanceDto.RoomId,
            RequestedBy = maintenanceDto.RequestedBy,
            Title = maintenanceDto.Title,
            Description = maintenanceDto.Description,
            Priority = maintenanceDto.Priority,
            RequestDate = DateTime.Now,
            Status = maintenanceDto.Status
        };

        var created = await _repository.AddAsync(request);

        maintenanceDto.MaintenanceRequestId = created.MaintenanceRequestId;
        maintenanceDto.RequestDate = created.RequestDate;

        return maintenanceDto;
    }

    public async Task<MaintenanceDto?> UpdateAsync(int id, MaintenanceDto maintenanceDto)
    {
        var request = await _repository.GetByIdAsync(id);

        if (request == null)
            return null;

        request.RoomId = maintenanceDto.RoomId;
        request.Title = maintenanceDto.Title;
        request.Description = maintenanceDto.Description;
        request.Priority = maintenanceDto.Priority;
        request.Status = maintenanceDto.Status;

        await _repository.UpdateAsync(request);

        return maintenanceDto;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        return await _repository.DeleteAsync(id);
    }
}