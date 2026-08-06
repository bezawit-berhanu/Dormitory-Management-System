using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class MaintenanceController : ControllerBase
{
    private readonly IMaintenanceService _maintenanceService;

    public MaintenanceController(IMaintenanceService maintenanceService)
    {
        _maintenanceService = maintenanceService;
    }


    // GET: api/Maintenance
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var requests = await _maintenanceService.GetAllAsync();

        return Ok(requests);
    }


    // GET: api/Maintenance/{id}
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var request = await _maintenanceService.GetByIdAsync(id);

        if (request == null)
            return NotFound();

        return Ok(request);
    }


    // POST: api/Maintenance
    [HttpPost]
    public async Task<IActionResult> Create(MaintenanceDto maintenanceDto)
    {
        var created = await _maintenanceService.CreateAsync(maintenanceDto);

        return CreatedAtAction(
            nameof(GetById),
            new { id = created.MaintenanceRequestId },
            created
        );
    }


    // PUT: api/Maintenance/{id}
    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        MaintenanceDto maintenanceDto)
    {
        var updated = await _maintenanceService.UpdateAsync(id, maintenanceDto);

        if (updated == null)
            return NotFound();

        return Ok(updated);
    }


    // DELETE: api/Maintenance/{id}
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _maintenanceService.DeleteAsync(id);

        if (!result)
            return NotFound();

        return NoContent();
    }
}