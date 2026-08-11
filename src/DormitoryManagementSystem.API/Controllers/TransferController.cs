using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TransferController : ControllerBase
{
    private readonly ITransferService _transferService;

    public TransferController(ITransferService transferService)
    {
        _transferService = transferService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _transferService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _transferService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(TransferDto dto)
    {
        var result = await _transferService.CreateAsync(dto);

        return Ok(new
        {
            sId = result.SId,
            reason = result.Reason,
            status = result.Status,
            currentRoomId = result.CurrentRoomId,
            requestedRoomId = result.RequestedRoomId,
            requestDate = result.RequestDate
        });
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, TransferDto dto)
    {
        var success = await _transferService.UpdateAsync(id, dto);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _transferService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}