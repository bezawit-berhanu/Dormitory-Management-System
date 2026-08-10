using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class RoomAssignmentRepository : IRoomAssignmentRepository
{
    private readonly ApplicationDbContext _context;

    public RoomAssignmentRepository (ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<RoomAssignment>> GetAllAsync()
    {
        return await _context.RoomAssignments
            .Include(r => r.Student)
            .Include(r => r.Room)
            .Include(r => r.Bed)
            .ToListAsync();
    }


    public async Task<RoomAssignment?> GetByIdAsync(int id)
    {
        return await _context.RoomAssignments
            .Include(r => r.Student)
            .Include(r => r.Room)
            .Include(r => r.Bed)
            .FirstOrDefaultAsync(r => r.RoomAssignmentId == id);
    }


    public async Task<IEnumerable<RoomAssignment>> GetByStudentIdAsync(int studentId)
    {
        return await _context.RoomAssignments
            .Where(r => r.SId == studentId)
            .ToListAsync();
    }


    public async Task AddAsync(RoomAssignment assignment)
    {
        await _context.RoomAssignments.AddAsync(assignment);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(RoomAssignment assignment)
    {
        _context.RoomAssignments.Update(assignment);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(RoomAssignment assignment)
    {
        _context.RoomAssignments.Remove(assignment);
        await _context.SaveChangesAsync();
    }


    public async Task<bool> ExistsAsync(int id)
    {
        return await _context.RoomAssignments
            .AnyAsync(r => r.RoomAssignmentId == id);
    }
}