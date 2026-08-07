using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IDormitoryStructureService
{
    // Dormitory
    Task<IEnumerable<DormitoryDto>> GetAllDormitoriesAsync();
    Task<DormitoryDto?> GetDormitoryByIdAsync(int id);
    Task<DormitoryDto> CreateDormitoryAsync(DormitoryDto dto);
    Task<bool> UpdateDormitoryAsync(int id, DormitoryDto dto);
    Task<bool> DeleteDormitoryAsync(int id);

    // Block
    Task<IEnumerable<BlockDto>> GetAllBlocksAsync();
    Task<BlockDto?> GetBlockByIdAsync(int id);
    Task<BlockDto> CreateBlockAsync(BlockDto dto);
    Task<bool> UpdateBlockAsync(int id, BlockDto dto);
    Task<bool> DeleteBlockAsync(int id);

    // Floor
    Task<IEnumerable<FloorDto>> GetAllFloorsAsync();
    Task<FloorDto?> GetFloorByIdAsync(int id);
    Task<FloorDto> CreateFloorAsync(FloorDto dto);
    Task<bool> UpdateFloorAsync(int id, FloorDto dto);
    Task<bool> DeleteFloorAsync(int id);

    // Room
    Task<IEnumerable<RoomDto>> GetAllRoomsAsync();
    Task<RoomDto?> GetRoomByIdAsync(int id);
    Task<RoomDto> CreateRoomAsync(RoomDto dto);
    Task<bool> UpdateRoomAsync(int id, RoomDto dto);
    Task<bool> DeleteRoomAsync(int id);

    // Bed
    Task<IEnumerable<BedDto>> GetAllBedsAsync();
    Task<BedDto?> GetBedByIdAsync(int id);
    Task<BedDto> CreateBedAsync(BedDto dto);
    Task<bool> UpdateBedAsync(int id, BedDto dto);
    Task<bool> DeleteBedAsync(int id);
}