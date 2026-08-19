using DormitoryManagementSystem.Domain.Enums;

namespace DormitoryManagementSystem.Application.DTOs;

public class UpdateStudentDto
{
    public int SId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string EmergencyContactNumber { get; set; } = string.Empty;
    public int YearOfStudy { get; set; }
    public UserStatus Status { get; set; } = UserStatus.Active;
}
