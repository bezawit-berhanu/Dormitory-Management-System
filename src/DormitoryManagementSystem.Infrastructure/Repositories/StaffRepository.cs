using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class StaffRepository : IStaffRepository
{
    private readonly ApplicationDbContext _context;

    public StaffRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Staff>> GetAllAsync()
    {
        return await _context.Staff
            .Include(s => s.User)
            .ToListAsync();
    }

    public async Task<Staff?> GetByIdAsync(int id)
    {
        return await _context.Staff
            .Include(s => s.User)
            .FirstOrDefaultAsync(s => s.StaffId == id);
    }

    public async Task<Staff?> GetByEmployeeIdAsync(
        string employeeId)
    {
        return await _context.Staff
            .Include(s => s.User)
            .FirstOrDefaultAsync(
                s => s.EmployeeId == employeeId);
    }
public async Task<Staff?> GetByUserIdAsync(int userId)
{
    return await _context.Staff
        .Include(s => s.User)
        .FirstOrDefaultAsync(s => s.UserId == userId);
}
    public async Task AddAsync(Staff staff)
    {
        await _context.Staff.AddAsync(staff);
    }

    public Task UpdateAsync(Staff staff)
    {
        _context.Staff.Update(staff);

        return Task.CompletedTask;
    }

    public async Task DeleteAsync(Staff staff)
    {
        _context.Staff.Remove(staff);

        await Task.CompletedTask;
    }

    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.Staff
            .AnyAsync(s => s.StaffId == id);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}