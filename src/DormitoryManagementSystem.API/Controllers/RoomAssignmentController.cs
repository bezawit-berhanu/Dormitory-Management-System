using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class RoomAssignmentController : ControllerBase
{
    private readonly IRoomAssignmentService _service;


    public RoomAssignmentController(IRoomAssignmentService service)
    {
        _service = service;
    }



    // Get all room assignments for a student
    [HttpGet("student/{studentId}")]
    public async Task<IActionResult> GetStudentAssignments(int studentId)
    {
        var assignments = await _service.GetStudentAssignmentsAsync(studentId);

        return Ok(assignments);
    }



    // Get assignment by id
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var assignment = await _service.GetAssignmentByIdAsync(id);


        if(assignment == null)
            return NotFound();


        return Ok(assignment);
    }



    // Assign room
    [HttpPost]
    public async Task<IActionResult> Assign(RoomAssignmentDto dto)
    {
        var assignment = await _service.AssignRoomAsync(dto);

        return Ok(assignment);
    }



    // Update assignment
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        RoomAssignmentDto dto)
    {
        var result = await _service.UpdateAssignmentAsync(id, dto);


        if(!result)
            return NotFound();


        return Ok("Room assignment updated successfully");
    }



    // Delete assignment
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteAssignmentAsync(id);


        if(!result)
            return NotFound();


        return Ok("Room assignment deleted successfully");
    }
}