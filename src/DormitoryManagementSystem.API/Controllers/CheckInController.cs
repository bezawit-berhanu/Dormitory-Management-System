using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class CheckInController : ControllerBase
{
    private readonly ICheckInService _service;


    public CheckInController(ICheckInService service)
    {
        _service = service;
    }



    // Student check-in history
    [HttpGet("history/{studentId}")]
    public async Task<IActionResult> GetHistory(int studentId)
    {
        var history = await _service.GetCheckInHistoryAsync(studentId);

        return Ok(history);
    }



    // Check student in
    [HttpPost]
    public async Task<IActionResult> CheckIn(CheckInDto dto)
    {
        var result = await _service.CheckInStudentAsync(dto);

        return Ok(result);
    }
}