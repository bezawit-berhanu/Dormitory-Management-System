using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class User
{
    public int UserId { get; set; }

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PasswordHash { get; set; } = string.Empty;

    public string? PhoneNumber { get; set; }

    public int RoleId { get; set; }

    public UserStatus Status { get; set; } =UserStatus.Active;

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;


    // Navigation

    public Role? Role { get; set; }


    public ICollection<RoomAssignment> AssignedRooms { get; set; }
        = new List<RoomAssignment>();


    public ICollection<CheckIn> CheckIns { get; set; }
        = new List<CheckIn>();


    public ICollection<CheckOut> CheckOuts { get; set; }
        = new List<CheckOut>();
}