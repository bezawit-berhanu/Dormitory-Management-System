using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Domain.Entities;


namespace DormitoryManagementSystem.Application.Services;


public class NotificationService : INotificationService
{

    private readonly INotificationRepository _repository;


    public NotificationService(INotificationRepository repository)
    {
        _repository = repository;
    }



    public async Task<IEnumerable<NotificationDto>> GetAllAsync()
    {
        var notifications = await _repository.GetAllAsync();

        return notifications.Select(n => new NotificationDto
        {
            NotificationId = n.NotificationId,
            UserId = n.UserId,
            Message = n.Message,
            IsRead = n.IsRead,
            CreatedDate = n.NotificationDate
        });
    }



    public async Task<NotificationDto?> GetByIdAsync(int id)
    {
        var n = await _repository.GetByIdAsync(id);

        if (n == null) return null;

        return new NotificationDto
        {
            NotificationId = n.NotificationId,
            UserId = n.UserId,
            Message = n.Message,
            IsRead = n.IsRead,
            CreatedDate = n.NotificationDate
        };
    }



    public async Task<IEnumerable<NotificationDto>> GetUserNotificationsAsync(int userId)
    {
        var notifications = await _repository.GetByUserIdAsync(userId);

        return notifications.Select(n => new NotificationDto
        {
            NotificationId = n.NotificationId,
            UserId = n.UserId,
            Message = n.Message,
            IsRead = n.IsRead,
            CreatedDate = n.NotificationDate
        });
    }



    public async Task<NotificationDto> CreateAsync(NotificationDto dto)
    {
        var notification = new Notification
        {
            UserId = dto.UserId,
            Message = dto.Message,
            NotificationDate = DateTime.Now,
            IsRead = false
        };

        await _repository.AddAsync(notification);

        dto.NotificationId = notification.NotificationId;
        dto.IsRead = notification.IsRead;
        dto.CreatedDate = notification.NotificationDate;

        return dto;
    }



    public async Task MarkAsReadAsync(int id)
    {
        var notification =
            await _repository.GetByIdAsync(id);


        if (notification != null)
        {
            notification.IsRead = true;

            await _repository.UpdateAsync(notification);
        }
    }



    public async Task DeleteAsync(int id)
    {
        await _repository.DeleteAsync(id);
    }

}