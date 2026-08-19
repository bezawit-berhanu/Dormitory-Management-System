using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IViolationRepository
{
    Task<IEnumerable<Violation>> GetAllAsync();

    Task<Violation?> GetByIdAsync(int id);

    Task AddAsync(Violation violation);

    Task UpdateAsync(Violation violation);

    Task DeleteAsync(int id);
}