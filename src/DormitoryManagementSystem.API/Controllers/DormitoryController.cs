using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class DormitoryStructureController : ControllerBase
{
    private readonly IDormitoryStructureService _service;

    public DormitoryStructureController(IDormitoryStructureService service)
    {
        _service = service;
    }
    [HttpGet("dormitories")]
    public async Task<IActionResult> GetDormitories()
    {
        var result = await _service.GetAllDormitoriesAsync();
        return Ok(result);
    }


    [HttpGet("dormitories/{id}")]
    public async Task<IActionResult> GetDormitoryById(int id)
    {
        var result = await _service.GetDormitoryByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPost("dormitories")]
    public async Task<IActionResult> CreateDormitory(DormitoryDto dto)
    {
        var result = await _service.CreateDormitoryAsync(dto);

        return Ok(result);
    }


    [HttpPut("dormitories/{id}")]
    public async Task<IActionResult> UpdateDormitory(int id, DormitoryDto dto)
    {
        var result = await _service.UpdateDormitoryAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }


    [HttpDelete("dormitories/{id}")]
    public async Task<IActionResult> DeleteDormitory(int id)
    {
        var result = await _service.DeleteDormitoryAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
    [HttpGet("blocks")]
    public async Task<IActionResult> GetBlocks()
    {
        var result = await _service.GetAllBlocksAsync();
        return Ok(result);
    }


    [HttpGet("blocks/{id}")]
    public async Task<IActionResult> GetBlockById(int id)
    {
        var result = await _service.GetBlockByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPost("blocks")]
    public async Task<IActionResult> CreateBlock(BlockDto dto)
    {
        var result = await _service.CreateBlockAsync(dto);

        return Ok(result);
    }


    [HttpPut("blocks/{id}")]
    public async Task<IActionResult> UpdateBlock(int id, BlockDto dto)
    {
        var result = await _service.UpdateBlockAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }


    [HttpDelete("blocks/{id}")]
    public async Task<IActionResult> DeleteBlock(int id)
    {
        var result = await _service.DeleteBlockAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
    [HttpGet("floors")]
    public async Task<IActionResult> GetFloors()
    {
        var result = await _service.GetAllFloorsAsync();
        return Ok(result);
    }


    [HttpGet("floors/{id}")]
    public async Task<IActionResult> GetFloorById(int id)
    {
        var result = await _service.GetFloorByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPost("floors")]
    public async Task<IActionResult> CreateFloor(FloorDto dto)
    {
        var result = await _service.CreateFloorAsync(dto);

        return Ok(result);
    }


    [HttpPut("floors/{id}")]
    public async Task<IActionResult> UpdateFloor(int id, FloorDto dto)
    {
        var result = await _service.UpdateFloorAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }


    [HttpDelete("floors/{id}")]
    public async Task<IActionResult> DeleteFloor(int id)
    {
        var result = await _service.DeleteFloorAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
    [HttpGet("rooms")]
    public async Task<IActionResult> GetRooms()
    {
        var result = await _service.GetAllRoomsAsync();
        return Ok(result);
    }


    [HttpGet("rooms/{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var result = await _service.GetRoomByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPost("rooms")]
    public async Task<IActionResult> CreateRoom(RoomDto dto)
    {
        var result = await _service.CreateRoomAsync(dto);

        return Ok(result);
    }


    [HttpPut("rooms/{id}")]
    public async Task<IActionResult> UpdateRoom(int id, RoomDto dto)
    {
        var result = await _service.UpdateRoomAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }


    [HttpDelete("rooms/{id}")]
    public async Task<IActionResult> DeleteRoom(int id)
    {
        var result = await _service.DeleteRoomAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
    [HttpGet("beds")]
    public async Task<IActionResult> GetBeds()
    {
        var result = await _service.GetAllBedsAsync();
        return Ok(result);
    }


    [HttpGet("beds/{id}")]
    public async Task<IActionResult> GetBedById(int id)
    {
        var result = await _service.GetBedByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }


    [HttpPost("beds")]
    public async Task<IActionResult> CreateBed(BedDto dto)
    {
        var result = await _service.CreateBedAsync(dto);

        return Ok(result);
    }


    [HttpPut("beds/{id}")]
    public async Task<IActionResult> UpdateBed(int id, BedDto dto)
    {
        var result = await _service.UpdateBedAsync(id, dto);

        if (!result)
            return NotFound();

        return NoContent();
    }


    [HttpDelete("beds/{id}")]
    public async Task<IActionResult> DeleteBed(int id)
    {
        var result = await _service.DeleteBedAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}