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



    [HttpPost]
    public async Task<IActionResult> Create(CreateStudentDto dto)
    {
        var student = await _service.CreateStudentAsync(dto);

        return Ok(student);
    }



    [HttpPut("{id}")]
    public async Task<IActionResult> Update(
        int id,
        UpdateStudentDto dto)
    {

        var result = await _service.UpdateStudentAsync(id,dto);


        if(!result)
            return NotFound();


        return Ok("Student updated");
    }



    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _service.DeleteStudentAsync(id);


        if(!result)
            return NotFound();


        return Ok("Student deleted");
    }
}