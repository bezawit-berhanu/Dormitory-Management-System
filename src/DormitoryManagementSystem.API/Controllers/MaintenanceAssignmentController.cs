using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceAssignmentController : ControllerBase
{
    private readonly IMaintenanceAssignmentService _service;

    public MaintenanceAssignmentController(IMaintenanceAssignmentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var assignments = await _service.GetAllAssignmentsAsync();
        return Ok(assignments);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var assignment = await _service.GetAssignmentByIdAsync(id);

        if (assignment == null)
            return NotFound();

        return Ok(assignment);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MaintenanceAssignmentDto dto)
    {
        var result = await _service.CreateAssignmentAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MaintenanceAssignmentDto dto)
    {
        var updated = await _service.UpdateAssignmentAsync(id, dto);

        if (!updated)
            return NotFound();

        return Ok("Maintenance assignment updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAssignmentAsync(id);

        if (!deleted)
            return NotFound();

        return Ok("Maintenance assignment deleted successfully");
    }
}