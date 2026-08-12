using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IRegistrarService
{
    Task<IEnumerable<RegistrarStudentDto>>
        GetStudentsAsync();

    Task<RegistrarStudentDto?>
        GetStudentByIdAsync(string studentId);

    Task<IEnumerable<RegistrarStudentDto>>
        SearchStudentsAsync(string query);
}