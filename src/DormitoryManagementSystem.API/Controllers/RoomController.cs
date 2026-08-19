using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RoomController : ControllerBase
{
private readonly IRoomService _service;

public RoomController(IRoomService service)
 {
 _service = service;
}

 [HttpGet]
public async Task<IActionResult> GetAll()
{
 var items = await _service.GetAllAsync();
 return Ok(items);
 }

[HttpGet("{id}")]
 public async Task<IActionResult> GetById(int id)
 {
var item = await _service.GetByIdAsync(id);
if (item == null) return NotFound();
 return Ok(item);
}

[HttpPost]
 public async Task<IActionResult> Create([FromBody] RoomDto dto)
 {
var created = await _service.CreateAsync(dto);
return CreatedAtAction(nameof(GetById), new { id = created.RoomId }, created);
 }
}

//room controller