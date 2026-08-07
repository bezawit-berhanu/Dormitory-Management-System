using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SecurityIncidentController : ControllerBase
{
    private readonly ISecurityIncidentService _service;

    public SecurityIncidentController(ISecurityIncidentService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var incidents = await _service.GetAllIncidentsAsync();
        return Ok(incidents);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var incident = await _service.GetIncidentByIdAsync(id);

        if (incident == null)
            return NotFound();

        return Ok(incident);
    }

    [HttpPost]
    public async Task<IActionResult> Create(SecurityIncidentDto dto)
    {
        var result = await _service.CreateIncidentAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, SecurityIncidentDto dto)
    {
        var updated = await _service.UpdateIncidentAsync(id, dto);

        if (!updated)
            return NotFound();

        return Ok("Security incident updated successfully");
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteIncidentAsync(id);

        if (!deleted)
            return NotFound();

        return Ok("Security incident deleted successfully");
    }
}