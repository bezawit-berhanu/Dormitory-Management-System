using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Services;
using DormitoryManagementSystem.Application.Interfaces;
namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ComplaintController : ControllerBase
{
    private readonly IComplaintService _service;

    public ComplaintController(IComplaintService service)
    {
        _service = service;
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var complaints = await _service.GetAllAsync();
        return Ok(complaints);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var complaint = await _service.GetByIdAsync(id);

        if (complaint == null)
            return NotFound();

        return Ok(complaint);
    }

    [HttpPost]
    public async Task<IActionResult> Create(ComplaintDto dto)
    {
        var result = await _service.CreateAsync(dto);
        return Ok(result);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, ComplaintDto dto)
    {
        var updated = await _service.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _service.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}