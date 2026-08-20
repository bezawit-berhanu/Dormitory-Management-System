using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface IStudentRepository
{
    Task<IEnumerable<Student>> GetAllAsync();

    Task<Student?> GetByIdAsync(int id);

    Task<Student?> GetByStudentIdAsync(string studentId);

    Task AddAsync(Student student);

    Task UpdateAsync(Student student);

    Task DeleteAsync(Student student);

    Task<bool> ExistsAsync(int id);

    Task SaveChangesAsync();
}