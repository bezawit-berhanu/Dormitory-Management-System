using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface INotificationService
{
    Task<IEnumerable<NotificationDto>> GetAllAsync();

    Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(int userId);

    Task<NotificationDto?> GetByIdAsync(int id);

    Task<NotificationDto> CreateAsync(NotificationDto dto);

    Task MarkAsReadAsync(int id);

    Task DeleteAsync(int id);
}