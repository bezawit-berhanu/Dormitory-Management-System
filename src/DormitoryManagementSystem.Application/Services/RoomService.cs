using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class RoomService : IRoomService
{
    private readonly IRoomRepository _repo;

    public RoomService(IRoomRepository repo)
    {
        _repo = repo;
    }

    public async Task<IEnumerable<RoomDto>> GetAllAsync()
    {
        var items = await _repo.GetAllAsync();
        return items.Select(r => new RoomDto
        {
            RoomId = r.RoomId,
            FloorId = r.FloorId,
            RoomNumber = r.RoomNumber,
            Capacity = r.Capacity,
            AvailableBeds = (r.Beds?.Count() ?? 0) - (r.RoomAssignments?.Count() ?? 0)
        });
    }

    public async Task<RoomDto?> GetByIdAsync(int id)
    {
        var r = await _repo.GetByIdAsync(id);
        if (r == null) return null;
        return new RoomDto
        {
            RoomId = r.RoomId,
            FloorId = r.FloorId,
            RoomNumber = r.RoomNumber,
            Capacity = r.Capacity,
            AvailableBeds = (r.Beds?.Count() ?? 0) - (r.RoomAssignments?.Count() ?? 0)
        };
    }

    public async Task<RoomDto> CreateAsync(RoomDto dto)
    {
        var entity = new Room
        {
            FloorId = dto.FloorId,
            RoomNumber = dto.RoomNumber,
            Capacity = dto.Capacity
        };

        await _repo.AddAsync(entity);
        await _repo.SaveChangesAsync();

        dto.RoomId = entity.RoomId;
        return dto;
    }
}