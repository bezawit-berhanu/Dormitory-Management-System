using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class DormitoryStructureRepository : IDormitoryStructureRepository
{
    private readonly ApplicationDbContext _context;

    public DormitoryStructureRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    // Dormitory
    public async Task<IEnumerable<Dormitory>> GetAllDormitoriesAsync()
    {
        return await _context.Dormitories.ToListAsync();
    }

    public async Task<Dormitory?> GetDormitoryByIdAsync(int id)
    {
        return await _context.Dormitories.FindAsync(id);
    }

    public async Task<Dormitory> AddDormitoryAsync(Dormitory dormitory)
    {
        await _context.Dormitories.AddAsync(dormitory);
        await _context.SaveChangesAsync();
        return dormitory;
    }

    public async Task UpdateDormitoryAsync(Dormitory dormitory)
    {
        _context.Dormitories.Update(dormitory);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteDormitoryAsync(int id)
    {
        var dormitory = await _context.Dormitories.FindAsync(id);

        if (dormitory == null)
            return false;

        _context.Dormitories.Remove(dormitory);
        await _context.SaveChangesAsync();

        return true;
    }

    // Block
    public async Task<IEnumerable<Block>> GetAllBlocksAsync()
    {
        return await _context.Blocks.ToListAsync();
    }

    public async Task<Block?> GetBlockByIdAsync(int id)
    {
        return await _context.Blocks.FindAsync(id);
    }

    public async Task<Block> AddBlockAsync(Block block)
    {
        await _context.Blocks.AddAsync(block);
        await _context.SaveChangesAsync();
        return block;
    }

    public async Task UpdateBlockAsync(Block block)
    {
        _context.Blocks.Update(block);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteBlockAsync(int id)
    {
        var block = await _context.Blocks.FindAsync(id);

        if (block == null)
            return false;

        _context.Blocks.Remove(block);
        await _context.SaveChangesAsync();

        return true;
    }

    // Floor
    public async Task<IEnumerable<Floor>> GetAllFloorsAsync()
    {
        return await _context.Floors.ToListAsync();
    }

    public async Task<Floor?> GetFloorByIdAsync(int id)
    {
        return await _context.Floors.FindAsync(id);
    }

    public async Task<Floor> AddFloorAsync(Floor floor)
    {
        await _context.Floors.AddAsync(floor);
        await _context.SaveChangesAsync();
        return floor;
    }

    public async Task UpdateFloorAsync(Floor floor)
    {
        _context.Floors.Update(floor);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteFloorAsync(int id)
    {
        var floor = await _context.Floors.FindAsync(id);

        if (floor == null)
            return false;

        _context.Floors.Remove(floor);
        await _context.SaveChangesAsync();

        return true;
    }

    // Room
    public async Task<IEnumerable<Room>> GetAllRoomsAsync()
    {
        return await _context.Rooms.ToListAsync();
    }

    public async Task<Room?> GetRoomByIdAsync(int id)
    {
        return await _context.Rooms.FindAsync(id);
    }

    public async Task<Room> AddRoomAsync(Room room)
    {
        await _context.Rooms.AddAsync(room);
        await _context.SaveChangesAsync();
        return room;
    }

    public async Task UpdateRoomAsync(Room room)
    {
        _context.Rooms.Update(room);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteRoomAsync(int id)
    {
        var room = await _context.Rooms.FindAsync(id);

        if (room == null)
            return false;

        _context.Rooms.Remove(room);
        await _context.SaveChangesAsync();

        return true;
    }

    // Bed
    public async Task<IEnumerable<Bed>> GetAllBedsAsync()
    {
        return await _context.Beds.ToListAsync();
    }

    public async Task<Bed?> GetBedByIdAsync(int id)
    {
        return await _context.Beds.FindAsync(id);
    }

    public async Task<Bed> AddBedAsync(Bed bed)
    {
        await _context.Beds.AddAsync(bed);
        await _context.SaveChangesAsync();
        return bed;
    }

    public async Task UpdateBedAsync(Bed bed)
    {
        _context.Beds.Update(bed);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteBedAsync(int id)
    {
        var bed = await _context.Beds.FindAsync(id);

        if (bed == null)
            return false;

        _context.Beds.Remove(bed);
        await _context.SaveChangesAsync();

        return true;
    }
}