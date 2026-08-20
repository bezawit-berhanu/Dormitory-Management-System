using Microsoft.AspNetCore.Mvc;
using MockRegistrarAPI.Data;
using MockRegistrarAPI.Models;

namespace MockRegistrarAPI.Controllers;

[ApiController]
[Route("api/students")]
public class StudentsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetStudents()
    {
        return Ok(MockStudentData.Students);
    }

    [HttpGet("{studentId}")]
    public IActionResult GetStudent(string studentId)
    {
        var student = MockStudentData.Students
            .FirstOrDefault(s =>
                s.StudentId.Equals(
                    studentId,
                    StringComparison.OrdinalIgnoreCase));

        if (student == null)
            return NotFound(new
            {
                message = "Student not found in Registrar."
            });

        return Ok(student);
    }


    [HttpGet("search")]
    public IActionResult SearchStudents(
        [FromQuery] string query)
    {
        if (string.IsNullOrWhiteSpace(query))
            return Ok(MockStudentData.Students);

        var students = MockStudentData.Students
            .Where(s =>
                s.StudentId.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase)
                ||
                s.FullName.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase)
                ||
                s.Department.Contains(
                    query,
                    StringComparison.OrdinalIgnoreCase))
            .ToList();

        return Ok(students);
    }
}
