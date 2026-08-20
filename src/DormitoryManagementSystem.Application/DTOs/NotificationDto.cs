using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Application.DTOs;

public class NotificationDto
{
    public int NotificationId { get; set; }

    [Required]
    public int UserId { get; set; }

    [Required]
    [StringLength(200)]
    public string Message { get; set; } = string.Empty;

    public bool IsRead { get; set; } = false;

    public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
}