namespace DormitoryManagementSystem.Application.DTOs;

public class RegistrarStaffDto
{
    public string EmployeeId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Campus { get; set; } = string.Empty;

    public string Role { get; set; } = string.Empty;

    public string? AssignedBlock { get; set; }

    public int Status { get; set; }
}