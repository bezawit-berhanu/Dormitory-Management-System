using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IStudentService
{
    Task<IEnumerable<StudentDto>> GetAllStudentsAsync();

    Task<StudentDto?> GetStudentByIdAsync(int id);

    Task<StudentDto> CreateStudentAsync(CreateStudentDto dto);

    Task<bool> UpdateStudentAsync(int id, UpdateStudentDto dto);

    Task<bool> DeleteStudentAsync(int id);
}