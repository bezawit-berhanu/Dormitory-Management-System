using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Services;

public class DormitoryStructureService : IDormitoryStructureService
{
    private readonly IDormitoryStructureRepository _repository;

    public DormitoryStructureService(IDormitoryStructureRepository repository)
    {
        _repository = repository;
    }
    public async Task<IEnumerable<DormitoryDto>> GetAllDormitoriesAsync()
    {
        var dormitories = await _repository.GetAllDormitoriesAsync();

        return dormitories.Select(d => new DormitoryDto
        {
            DormitoryId = d.DormitoryId,
            DormitoryName = d.DormitoryName,
            Location = d.Location
        });
    }


    public async Task<DormitoryDto?> GetDormitoryByIdAsync(int id)
    {
        var dormitory = await _repository.GetDormitoryByIdAsync(id);

        if (dormitory == null)
            return null;

        return new DormitoryDto
        {
            DormitoryId = dormitory.DormitoryId,
            DormitoryName = dormitory.DormitoryName,
            Location = dormitory.Location
        };
    }


    public async Task<DormitoryDto> CreateDormitoryAsync(DormitoryDto dto)
    {
        var dormitory = new Dormitory
        {
            DormitoryName = dto.DormitoryName,
            Location = dto.Location
        };

        var created = await _repository.AddDormitoryAsync(dormitory);

        dto.DormitoryId = created.DormitoryId;

        return dto;
    }


    public async Task<bool> UpdateDormitoryAsync(int id, DormitoryDto dto)
    {
        var dormitory = await _repository.GetDormitoryByIdAsync(id);

        if (dormitory == null)
            return false;

        dormitory.DormitoryName = dto.DormitoryName;
        dormitory.Location = dto.Location;

        await _repository.UpdateDormitoryAsync(dormitory);

        return true;
    }


    public async Task<bool> DeleteDormitoryAsync(int id)
    {
        return await _repository.DeleteDormitoryAsync(id);
    }

    public async Task<IEnumerable<BlockDto>> GetAllBlocksAsync()
    {
        var blocks = await _repository.GetAllBlocksAsync();

        return blocks.Select(b => new BlockDto
        {
            BlockId = b.BlockId,
            BlockName = b.BlockName,
            DormitoryId = b.DormitoryId,
            Description = b.Description
        });
    }


    public async Task<BlockDto?> GetBlockByIdAsync(int id)
    {
        var block = await _repository.GetBlockByIdAsync(id);

        if (block == null)
            return null;

        return new BlockDto
        {
            BlockId = block.BlockId,
            BlockName = block.BlockName,
            DormitoryId = block.DormitoryId,
            Description = block.Description
        };
    }


    public async Task<BlockDto> CreateBlockAsync(BlockDto dto)
    {
        var block = new Block
        {
            BlockName = dto.BlockName,
            DormitoryId = dto.DormitoryId,
            Description = dto.Description
        };

        var created = await _repository.AddBlockAsync(block);

        dto.BlockId = created.BlockId;

        return dto;
    }


    public async Task<bool> UpdateBlockAsync(int id, BlockDto dto)
    {
        var block = await _repository.GetBlockByIdAsync(id);

        if (block == null)
            return false;

        block.BlockName = dto.BlockName;
        block.DormitoryId = dto.DormitoryId;
        block.Description = dto.Description;

        await _repository.UpdateBlockAsync(block);

        return true;
    }


    public async Task<bool> DeleteBlockAsync(int id)
    {
        return await _repository.DeleteBlockAsync(id);
    }
    public async Task<IEnumerable<FloorDto>> GetAllFloorsAsync()
    {
        var floors = await _repository.GetAllFloorsAsync();

        return floors.Select(f => new FloorDto
        {
            FloorId = f.FloorId,
            FloorNumber = f.FloorNumber,
            BlockId = f.BlockId,
            Description = f.Description
        });
    }


    public async Task<FloorDto?> GetFloorByIdAsync(int id)
    {
        var floor = await _repository.GetFloorByIdAsync(id);

        if (floor == null)
            return null;

        return new FloorDto
        {
            FloorId = floor.FloorId,
            FloorNumber = floor.FloorNumber,
            BlockId = floor.BlockId,
            Description = floor.Description
        };
    }


    public async Task<FloorDto> CreateFloorAsync(FloorDto dto)
    {
        var floor = new Floor
        {
            FloorNumber = dto.FloorNumber,
            BlockId = dto.BlockId,
            Description = dto.Description
        };

        var created = await _repository.AddFloorAsync(floor);

        dto.FloorId = created.FloorId;

        return dto;
    }


    public async Task<bool> UpdateFloorAsync(int id, FloorDto dto)
    {
        var floor = await _repository.GetFloorByIdAsync(id);

        if (floor == null)
            return false;

        floor.FloorNumber = dto.FloorNumber;
        floor.BlockId = dto.BlockId;
        floor.Description = dto.Description;

        await _repository.UpdateFloorAsync(floor);

        return true;
    }


    public async Task<bool> DeleteFloorAsync(int id)
    {
        return await _repository.DeleteFloorAsync(id);
    }
    public async Task<IEnumerable<RoomDto>> GetAllRoomsAsync()
    {
        var rooms = await _repository.GetAllRoomsAsync();

        return rooms.Select(r => new RoomDto
        {
            RoomId = r.RoomId,
            RoomNumber = r.RoomNumber,
            FloorId = r.FloorId,
            Capacity = r.Capacity,
            Status = r.Status
        });
    }


    public async Task<RoomDto?> GetRoomByIdAsync(int id)
    {
        var room = await _repository.GetRoomByIdAsync(id);

        if (room == null)
            return null;

        return new RoomDto
        {
            RoomId = room.RoomId,
            RoomNumber = room.RoomNumber,
            FloorId = room.FloorId,
            Capacity = room.Capacity,
            Status = room.Status
        };
    }


    public async Task<RoomDto> CreateRoomAsync(RoomDto dto)
    {
        var room = new Room
        {
            RoomNumber = dto.RoomNumber,
            FloorId = dto.FloorId,
            Capacity = dto.Capacity,
            Status = dto.Status
        };

        var created = await _repository.AddRoomAsync(room);

        dto.RoomId = created.RoomId;

        return dto;
    }


    public async Task<bool> UpdateRoomAsync(int id, RoomDto dto)
    {
        var room = await _repository.GetRoomByIdAsync(id);

        if (room == null)
            return false;

        room.RoomNumber = dto.RoomNumber;
        room.FloorId = dto.FloorId;
        room.Capacity = dto.Capacity;
        room.Status = dto.Status;

        await _repository.UpdateRoomAsync(room);

        return true;
    }


    public async Task<bool> DeleteRoomAsync(int id)
    {
        return await _repository.DeleteRoomAsync(id);
    }
    public async Task<IEnumerable<BedDto>> GetAllBedsAsync()
    {
        var beds = await _repository.GetAllBedsAsync();

        return beds.Select(b => new BedDto
        {
            BedId = b.BedId,
            BedNumber = b.BedNumber,
            RoomId = b.RoomId,
            Status = b.Status
        });
    }


    public async Task<BedDto?> GetBedByIdAsync(int id)
    {
        var bed = await _repository.GetBedByIdAsync(id);

        if (bed == null)
            return null;

        return new BedDto
        {
            BedId = bed.BedId,
            BedNumber = bed.BedNumber,
            RoomId = bed.RoomId,
            Status = bed.Status
        };
    }


    public async Task<BedDto> CreateBedAsync(BedDto dto)
    {
        var bed = new Bed
        {
            BedNumber = dto.BedNumber,
            RoomId = dto.RoomId,
            Status = dto.Status
        };

        var created = await _repository.AddBedAsync(bed);

        dto.BedId = created.BedId;

        return dto;
    }


    public async Task<bool> UpdateBedAsync(int id, BedDto dto)
    {
        var bed = await _repository.GetBedByIdAsync(id);

        if (bed == null)
            return false;

        bed.BedNumber = dto.BedNumber;
        bed.RoomId = dto.RoomId;
        bed.Status = dto.Status;

        await _repository.UpdateBedAsync(bed);

        return true;
    }


    public async Task<bool> DeleteBedAsync(int id)
    {
        return await _repository.DeleteBedAsync(id);
    }

}