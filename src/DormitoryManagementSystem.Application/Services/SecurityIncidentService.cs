using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Services;

public class SecurityIncidentService : ISecurityIncidentService
{
    private readonly ISecurityIncidentRepository _repository;

    public SecurityIncidentService(ISecurityIncidentRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<SecurityIncidentDto>> GetAllIncidentsAsync()
    {
        var incidents = await _repository.GetAllAsync();

        return incidents.Select(i => new SecurityIncidentDto
        {
            RoomId = i.RoomId,
            ReportedBy = i.ReportedBy,
            IncidentType = i.IncidentType,
            Description = i.Description,
            IncidentDate = i.IncidentDate,
            Status = i.Status
        });
    }

    public async Task<SecurityIncidentDto?> GetIncidentByIdAsync(int id)
    {
        var incident = await _repository.GetByIdAsync(id);

        if (incident == null)
            return null;

        return new SecurityIncidentDto
        {
            RoomId = incident.RoomId,
            ReportedBy = incident.ReportedBy,
            IncidentType = incident.IncidentType,
            Description = incident.Description,
            IncidentDate = incident.IncidentDate,
            Status = incident.Status
        };
    }

    public async Task<SecurityIncidentDto> CreateIncidentAsync(SecurityIncidentDto dto)
    {
        var incident = new SecurityIncident
        {
            RoomId = dto.RoomId,
            ReportedBy = dto.ReportedBy,
            IncidentType = dto.IncidentType,
            Description = dto.Description,
            IncidentDate = dto.IncidentDate,
            Status = dto.Status
        };

        await _repository.AddAsync(incident);

        return dto;
    }

    public async Task<bool> UpdateIncidentAsync(int id, SecurityIncidentDto dto)
    {
        var incident = await _repository.GetByIdAsync(id);

        if (incident == null)
            return false;

        incident.RoomId = dto.RoomId;
        incident.ReportedBy = dto.ReportedBy;
        incident.IncidentType = dto.IncidentType;
        incident.Description = dto.Description;
        incident.IncidentDate = dto.IncidentDate;
        incident.Status = dto.Status;

        await _repository.UpdateAsync(incident);

        return true;
    }

    public async Task<bool> DeleteIncidentAsync(int id)
    {
        var incident = await _repository.GetByIdAsync(id);

        if (incident == null)
            return false;

        await _repository.DeleteAsync(incident);

        return true;
    }
}