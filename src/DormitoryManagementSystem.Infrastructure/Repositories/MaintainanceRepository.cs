using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class MaintenanceRepository : IMaintenanceRepository
{
    private readonly ApplicationDbContext _context;

    public MaintenanceRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<MaintenanceRequest>> GetAllAsync()
    {
        return await _context.MaintenanceRequests
            .Include(m => m.Room)
            .Include(m => m.RequestedByUser)
            .ToListAsync();
    }

    public async Task<MaintenanceRequest?> GetByIdAsync(int id)
    {
        return await _context.MaintenanceRequests
            .Include(m => m.Room)
            .Include(m => m.RequestedByUser)
            .FirstOrDefaultAsync(m => m.MaintenanceRequestId == id);
    }

    public async Task<MaintenanceRequest> AddAsync(MaintenanceRequest request)
    {
        await _context.MaintenanceRequests.AddAsync(request);
        await _context.SaveChangesAsync();

        return request;
    }

    public async Task UpdateAsync(MaintenanceRequest request)
    {
        _context.MaintenanceRequests.Update(request);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var request = await _context.MaintenanceRequests
            .FirstOrDefaultAsync(m => m.MaintenanceRequestId == id);

        if (request == null)
            return false;

        _context.MaintenanceRequests.Remove(request);
        await _context.SaveChangesAsync();

        return true;
    }
}