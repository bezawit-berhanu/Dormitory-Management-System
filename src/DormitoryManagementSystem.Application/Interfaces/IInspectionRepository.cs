using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IInspectionRepository
{
    Task<IEnumerable<Inspection>> GetAllAsync();

    Task<Inspection?> GetByIdAsync(int id);

    Task AddAsync(Inspection inspection);

    Task UpdateAsync(Inspection inspection);

    Task DeleteAsync(int id);
}