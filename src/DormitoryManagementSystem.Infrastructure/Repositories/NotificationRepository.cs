using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Infrastructure.Repositories;


public class NotificationRepository : INotificationRepository
{

    private readonly List<Notification> _notifications = new();



    public Task<IEnumerable<Notification>> GetAllAsync()
    {
        return Task.FromResult(
            _notifications.AsEnumerable()
        );
    }



    public Task<Notification?> GetByIdAsync(int id)
    {
        return Task.FromResult(
            _notifications.FirstOrDefault(
                n => n.NotificationId == id
            )
        );
    }


    public Task<IEnumerable<Notification>> GetByUserIdAsync(int userId)
    {
        return Task.FromResult(
            _notifications.Where(n => n.UserId == userId)
        );
    }



    public Task AddAsync(Notification notification)
    {
        notification.NotificationId =
            _notifications.Count + 1;


        _notifications.Add(notification);

        return Task.CompletedTask;
    }



    public Task UpdateAsync(Notification notification)
    {
        return Task.CompletedTask;
    }



    public Task DeleteAsync(int id)
    {

        var notification =
            _notifications.FirstOrDefault(
                n => n.NotificationId == id
            );


        if (notification != null)
        {
            _notifications.Remove(notification);
        }


        return Task.CompletedTask;
    }
}