using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class MaintenanceAssignmentRepository : IMaintenanceAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceAssignmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceAssignment>> GetAllAsync()
    {
        return await _context.MaintenanceAssignments
            .Include(a => a.MaintenanceRequest)
            .Include(a => a.User)
            .ToListAsync();
    }

    public async Task<MaintenanceAssignment?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceAssignments
            .Include(a => a.MaintenanceRequest)
            .Include(a => a.User)
            .FirstOrDefaultAsync(a => a.AssignmentId == id);
    }

    public async Task AddAsync(MaintenanceAssignment assignment)
    {
        await _context.MaintenanceAssignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaintenanceAssignment assignment)
    {
        _context.MaintenanceAssignments.Update(assignment);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MaintenanceAssignment assignment)
    {
        _context.MaintenanceAssignments.Remove(assignment);
        await _context.SaveChangesAsync();
    }
}