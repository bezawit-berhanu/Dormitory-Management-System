using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class RegistrarController : ControllerBase
{
    private readonly IRegistrarService _registrarService;

    public RegistrarController(
        IRegistrarService registrarService)
    {
        _registrarService = registrarService;
    }

    [HttpGet("students")]
    public async Task<IActionResult> GetStudents()
    {
        var students =
            await _registrarService.GetStudentsAsync();

        return Ok(students);
    }

    

    [HttpGet("students/{studentId}")]
    public async Task<IActionResult> GetStudent(
        string studentId)
    {
        var student =
            await _registrarService
                .GetStudentByIdAsync(studentId);

        if (student == null)
            return NotFound();

        return Ok(student);
    }


    [HttpGet("students/search")]
    public async Task<IActionResult> Search(
        [FromQuery] string query)
    {
        var students =
            await _registrarService
                .SearchStudentsAsync(query);

        return Ok(students);
    }
}