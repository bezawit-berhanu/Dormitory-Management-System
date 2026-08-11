using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AnnouncementController : ControllerBase
{
    private readonly IAnnouncementService _announcementService;

    public AnnouncementController(IAnnouncementService announcementService)
    {
        _announcementService = announcementService;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var announcements = await _announcementService.GetAllAsync();
        return Ok(announcements);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var announcement = await _announcementService.GetByIdAsync(id);

        if (announcement == null)
            return NotFound();

        return Ok(announcement);
    }

    [HttpPost]
    public async Task<IActionResult> Create(AnnouncementDto dto)
    {
        var created = await _announcementService.CreateAsync(dto);
        return Ok(created);

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(int id, AnnouncementDto dto)
    {
        var updated = await _announcementService.UpdateAsync(id, dto);

        if (!updated)
            return NotFound();

        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var deleted = await _announcementService.DeleteAsync(id);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}