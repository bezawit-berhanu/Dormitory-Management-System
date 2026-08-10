using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CheckOutController : ControllerBase
{
    private readonly ICheckOutService _service;

    public CheckOutController(ICheckOutService service)
    {
        _service = service;
    }

    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetHistory(int studentId)
    {
        var history = await _service.GetCheckOutHistoryAsync(studentId);

        return Ok(history);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var checkOut = await _service.GetCheckOutByIdAsync(id);

        if (checkOut == null)
            return NotFound();

        return Ok(checkOut);
    }

    [HttpPost]
    public async Task<IActionResult> CheckOut(CheckOutDto dto)
    {
        var result = await _service.CheckOutStudentAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, CheckOutDto dto)
    {
        var result = await _service.UpdateCheckOutAsync(id, dto);

        if (!result)
            return NotFound();

        return Ok("Check-out updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteCheckOutAsync(id);

        if (!result)
            return NotFound();

        return Ok("Check-out deleted successfully");
    }
}