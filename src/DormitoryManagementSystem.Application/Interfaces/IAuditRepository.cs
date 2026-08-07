using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IAuditRepository
{
    Task<IEnumerable<AuditLogDto>> GetAllAsync();

    Task<AuditLogDto?> GetByIdAsync(int id);
}