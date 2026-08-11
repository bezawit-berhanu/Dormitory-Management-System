using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Application.Interfaces;
namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class InspectionRepository : IInspectionRepository
{
    private readonly ApplicationDbContext _context;

    public InspectionRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Inspection>> GetAllAsync()
    {
        return await _context.Inspections.ToListAsync();
    }

    public async Task<Inspection?> GetByIdAsync(int id)
    {
        return await _context.Inspections
            .FirstOrDefaultAsync(i => i.InspectionId == id);
    }

    public async Task AddAsync(Inspection inspection)
    {
        await _context.Inspections.AddAsync(inspection);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Inspection inspection)
    {
        _context.Inspections.Update(inspection);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var inspection = await _context.Inspections
            .FirstOrDefaultAsync(i => i.InspectionId == id);

        if (inspection != null)
        {
            _context.Inspections.Remove(inspection);
            await _context.SaveChangesAsync();
        }
    }
}