using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class SecurityIncidentRepository : ISecurityIncidentRepository
{
    private readonly ApplicationDbContext _context;

    public SecurityIncidentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<SecurityIncident>> GetAllAsync()
    {
        return await _context.SecurityIncidents
            .Include(i => i.Room)
            .Include(i => i.ReportedByUser)
            .ToListAsync();
    }

    public async Task<SecurityIncident?> GetByIdAsync(int id)
    {
        return await _context.SecurityIncidents
            .Include(i => i.Room)
            .Include(i => i.ReportedByUser)
            .FirstOrDefaultAsync(i => i.IncidentId == id);
    }

    public async Task AddAsync(SecurityIncident incident)
    {
        await _context.SecurityIncidents.AddAsync(incident);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(SecurityIncident incident)
    {
        _context.SecurityIncidents.Update(incident);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(SecurityIncident incident)
    {
        _context.SecurityIncidents.Remove(incident);
        await _context.SaveChangesAsync();
    }
}