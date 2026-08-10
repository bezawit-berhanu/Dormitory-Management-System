using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IDormitoryStructureRepository
{
    // Dormitory
    Task<IEnumerable<Dormitory>> GetAllDormitoriesAsync();
    Task<Dormitory?> GetDormitoryByIdAsync(int id);
    Task<Dormitory> AddDormitoryAsync(Dormitory dormitory);
    Task UpdateDormitoryAsync(Dormitory dormitory);
    Task<bool> DeactivateDormitoryAsync(int id);

    // Block
    Task<IEnumerable<Block>> GetAllBlocksAsync();
    Task<Block?> GetBlockByIdAsync(int id);
    Task<Block> AddBlockAsync(Block block);
    Task UpdateBlockAsync(Block block);
    Task<bool> DeactivateBlockAsync(int id);

    // Floor
    Task<IEnumerable<Floor>> GetAllFloorsAsync();
    Task<Floor?> GetFloorByIdAsync(int id);
    Task<Floor> AddFloorAsync(Floor floor);
    Task UpdateFloorAsync(Floor floor);
    Task<bool> DeactivateFloorAsync(int id);

    // Room
    Task<IEnumerable<Room>> GetAllRoomsAsync();
    Task<Room?> GetRoomByIdAsync(int id);
    Task<Room> AddRoomAsync(Room room);
    Task UpdateRoomAsync(Room room);
    Task<bool> DeactivateRoomAsync(int id);

    // Bed
    Task<IEnumerable<Bed>> GetAllBedsAsync();
    Task<Bed?> GetBedByIdAsync(int id);
    Task<Bed> AddBedAsync(Bed bed);
    Task UpdateBedAsync(Bed bed);
    Task<bool> DeactivateBedAsync(int id);
}