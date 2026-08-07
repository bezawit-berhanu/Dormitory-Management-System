using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceActivityController : ControllerBase
{
    private readonly IMaintenanceActivityService _service;

    public MaintenanceActivityController(IMaintenanceActivityService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var activities = await _service.GetAllActivitiesAsync();
        return Ok(activities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var activity = await _service.GetActivityByIdAsync(id);

        if (activity == null)
            return NotFound();

        return Ok(activity);
    }

    [HttpPost]
    public async Task<IActionResult> Create(MaintenanceActivityDto dto)
    {
        var result = await _service.CreateActivityAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, MaintenanceActivityDto dto)
    {
        var updated = await _service.UpdateActivityAsync(id, dto);

        if (!updated)
            return NotFound();

        return Ok("Maintenance activity updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteActivityAsync(id);

        if (!deleted)
            return NotFound();

        return Ok("Maintenance activity deleted successfully");
    }
}