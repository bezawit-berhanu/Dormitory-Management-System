using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ViolationController : ControllerBase
{
    private readonly IViolationService _violationService;

    public ViolationController(IViolationService violationService)
    {
        _violationService = violationService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _violationService.GetAllAsync());
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var result = await _violationService.GetByIdAsync(id);

        if (result == null)
            return NotFound();

        return Ok(result);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ViolationDto dto)
    {
        var result = await _violationService.CreateAsync(dto);

        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ViolationDto dto)
    {
        var success = await _violationService.UpdateAsync(id, dto);

        if (!success)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var success = await _violationService.DeleteAsync(id);

        if (!success)
            return NotFound();

        return NoContent();
    }
}

