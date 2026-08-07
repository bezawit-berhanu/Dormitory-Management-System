using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class MaintenanceActivityRepository : IMaintenanceActivityRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceActivityRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceActivity>> GetAllAsync()
    {
        return await _context.MaintenanceActivities
            .Include(a => a.MaintenanceRequest)
            .Include(a => a.PerformedByUser)
            .ToListAsync();
    }

    public async Task<MaintenanceActivity?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceActivities
            .Include(a => a.MaintenanceRequest)
            .Include(a => a.PerformedByUser)
            .FirstOrDefaultAsync(a => a.ActivityId == id);
    }

    public async Task AddAsync(MaintenanceActivity activity)
    {
        await _context.MaintenanceActivities.AddAsync(activity);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(MaintenanceActivity activity)
    {
        _context.MaintenanceActivities.Update(activity);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(MaintenanceActivity activity)
    {
        _context.MaintenanceActivities.Remove(activity);
        await _context.SaveChangesAsync();
    }
}