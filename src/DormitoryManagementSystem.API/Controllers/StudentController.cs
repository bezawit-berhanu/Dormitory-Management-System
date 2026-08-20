using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;


namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class StudentController : ControllerBase
{

    private readonly IStudentService _service;


    public StudentController(IStudentService service)
    {
        _service = service;
    }



    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var students = await _service.GetAllStudentsAsync();

        return Ok(students);
    }



    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var student = await _service.GetStudentByIdAsync(id);


        if(student == null)
            return NotFound();


        return Ok(student);
    }
}