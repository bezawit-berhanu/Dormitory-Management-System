using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomAssignmentController : ControllerBase
{
    private readonly IRoomAssignmentService _service;

    public RoomAssignmentController(
        IRoomAssignmentService service)
    {
        _service = service;
    }



    [HttpGet("student/{sId}")]
    public async Task<IActionResult> GetStudentAssignments(
        int sId)
    {
        var assignments =
            await _service.GetStudentAssignmentsAsync(sId);

        return Ok(assignments);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var assignment =
            await _service.GetAssignmentByIdAsync(id);

        if (assignment == null)
            return NotFound();

        return Ok(assignment);
    }



    [HttpPost]
    public async Task<IActionResult> Assign(
        RoomAssignmentDto dto)
    {
        var assignment =
            await _service.AssignRoomAsync(dto);

        return Ok(assignment);
    }


    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        RoomAssignmentDto dto)
    {
        var result =
            await _service.UpdateAssignmentAsync(
                id,
                dto);

        if (!result)
            return NotFound();

        return Ok(
            "Room assignment updated successfully");
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result =
            await _service.DeleteAssignmentAsync(id);

        if (!result)
            return NotFound();

        return Ok(
            "Room assignment deleted successfully");
    }
}
