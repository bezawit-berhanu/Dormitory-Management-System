using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;
using DormitoryManagementSystem.Application.Services;

using DormitoryManagementSystem.Application.Interfaces;
namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class ComplaintRepository : IComplaintRepository
{
    private readonly ApplicationDbContext _context;

    public ComplaintRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Complaint>> GetAllAsync()
    {
        return await _context.Complaints
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Complaint?> GetByIdAsync(int id)
    {
        return await _context.Complaints
            .FirstOrDefaultAsync(c => c.ComplaintId == id);
    }

    public async Task AddAsync(Complaint complaint)
    {
        await _context.Complaints.AddAsync(complaint);
        await _context.SaveChangesAsync();
    }

    public async Task UpdateAsync(Complaint complaint)
    {
        _context.Complaints.Update(complaint);
        await _context.SaveChangesAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var item = await _context.Complaints
            .FirstOrDefaultAsync(x => x.ComplaintId == id);

        if (item != null)
        {
            _context.Complaints.Remove(item);
            await _context.SaveChangesAsync();
        }
    }
}