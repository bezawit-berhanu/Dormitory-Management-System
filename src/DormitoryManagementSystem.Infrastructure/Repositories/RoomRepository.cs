using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class RoomRepository : IRoomRepository
{
    private readonly ApplicationDbContext _context;

    public RoomRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Room>> GetAllAsync()
    {
        return await _context.Rooms
            .Include(r => r.Beds)
            .ToListAsync();
    }

    public async Task<Room?> GetByIdAsync(int id)
    {
        return await _context.Rooms
            .Include(r => r.Beds)
            .FirstOrDefaultAsync(r => r.RoomId == id);
    }

    public async Task AddAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
    }

    public Task UpdateAsync(Room room)
    {
        _context.Rooms.Update(room);
        return Task.CompletedTask;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}