using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IAuditService
{
    Task<IEnumerable<AuditLogDto>> GetAllLogsAsync();

    Task<AuditLogDto?> GetLogByIdAsync(int id);
}