using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class AuditRepository : IAuditRepository
{
    private readonly ApplicationDbContext _context;

    public AuditRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<AuditLogDto>> GetAllAsync()
    {
        return await _context.AuditLogs
            .Select(a => new AuditLogDto
            {
                AuditLogId = a.AuditLogId,
                UserId = a.UserId,
                Action = a.Action,
                TableName = a.TableName,
                RecordId = a.RecordId,
                ActionDate = a.ActionDate
            })
            .ToListAsync();
    }

    public async Task<AuditLogDto?> GetByIdAsync(int id)
    {
        return await _context.AuditLogs
            .Where(a => a.AuditLogId == id)
            .Select(a => new AuditLogDto
            {
                AuditLogId = a.AuditLogId,
                UserId = a.UserId,
                Action = a.Action,
                TableName = a.TableName,
                RecordId = a.RecordId,
                ActionDate = a.ActionDate
            })
            .FirstOrDefaultAsync();
    }
}