using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface IUserRepository
{
    Task<User?> GetByEmailAsync(string email);

    Task<User?> GetByIdAsync(int id);

    Task<IEnumerable<User>> GetAllAsync();

    Task AddAsync(User user);

    Task UpdateAsync(User user);

    Task DeleteAsync(int id);

    Task SaveChangesAsync();
}