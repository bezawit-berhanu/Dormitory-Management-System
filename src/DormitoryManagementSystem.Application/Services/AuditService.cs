using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class AuditService : IAuditService
{
    private readonly IAuditRepository _repository;   // ADD HERE

    public AuditService(IAuditRepository repository) // ADD HERE
    {
        _repository = repository;                    // ADD HERE
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllLogsAsync()
    {
        var logs = new List<AuditLogDto>();

        return await Task.FromResult(logs);
    }

    public async Task<AuditLogDto?> GetLogByIdAsync(int id)
    {
        return await Task.FromResult<AuditLogDto?>(null);
    }
}