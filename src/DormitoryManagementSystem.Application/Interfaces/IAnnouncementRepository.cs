using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IAnnouncementRepository
{
    Task<IEnumerable<Announcement>> GetAllAsync();
    Task<Announcement?> GetByIdAsync(int id);
    Task<Announcement> AddAsync(Announcement announcement);
    Task UpdateAsync(Announcement announcement);
    Task DeleteAsync(int id);
}