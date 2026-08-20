namespace DormitoryManagementSystem.Domain.Entities;

public class Notification
{
    public int NotificationId { get; set; }   // Primary Key

    public int UserId { get; set; }            // FK
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime NotificationDate { get; set; }

    public bool IsRead { get; set; }

    public User User { get; set; } = null!;
};

