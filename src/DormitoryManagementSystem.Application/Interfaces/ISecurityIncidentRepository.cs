using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface ISecurityIncidentRepository
{
    Task<IEnumerable<SecurityIncident>> GetAllAsync();

    Task<SecurityIncident?> GetByIdAsync(int id);

    Task AddAsync(SecurityIncident incident);

    Task UpdateAsync(SecurityIncident incident);

    Task DeleteAsync(SecurityIncident incident);
}