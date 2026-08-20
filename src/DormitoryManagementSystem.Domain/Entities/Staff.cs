using DormitoryManagementSystem.Domain.Enums;

namespace DormitoryManagementSystem.Domain.Entities;

public class Staff
{
    public int StaffId { get; set; }

    public int UserId { get; set; }

    public string EmployeeId { get; set; } = string.Empty;

    public string Campus { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string? AssignedBlock { get; set; }

    public UserStatus Status { get; set; }


    
    // NAVIGATION
    

    public User? User { get; set; }
}