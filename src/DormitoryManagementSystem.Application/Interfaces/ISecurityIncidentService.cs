using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface ISecurityIncidentService
{
    Task<IEnumerable<SecurityIncidentDto>> GetAllIncidentsAsync();

    Task<SecurityIncidentDto?> GetIncidentByIdAsync(int id);

    Task<SecurityIncidentDto> CreateIncidentAsync(SecurityIncidentDto dto);

    Task<bool> UpdateIncidentAsync(int id, SecurityIncidentDto dto);

    Task<bool> DeleteIncidentAsync(int id);
}