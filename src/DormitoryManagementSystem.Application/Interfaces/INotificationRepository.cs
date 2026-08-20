using DormitoryManagementSystem.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface INotificationRepository
{
    Task<IEnumerable<Notification>> GetAllAsync();
    Task<Notification?> GetByIdAsync(int id);
    Task<IEnumerable<Notification>> GetByUserIdAsync(int userId);
    Task AddAsync(Notification notification);
    Task UpdateAsync(Notification notification);
    Task DeleteAsync(int id);
}
