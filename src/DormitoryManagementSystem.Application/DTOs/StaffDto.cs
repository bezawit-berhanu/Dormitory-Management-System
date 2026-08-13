namespace DormitoryManagementSystem.Application.DTOs;

public class StaffDto
{
    public int StaffId { get; set; }

    public int UserId { get; set; }

    public string EmployeeId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Campus { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string? AssignedBlock { get; set; }

    public bool IsActive { get; set; }
}