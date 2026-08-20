using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface IRoomRepository
{
    Task<IEnumerable<Room>> GetAllAsync();
    Task<Room?> GetByIdAsync(int id);
    Task AddAsync(Room room);
    Task UpdateAsync(Room room);
    Task SaveChangesAsync();
}