using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Application.DTOs;
public class UpdateStudentDto
{
    public string FullName { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
public int DepartmentId { get; set; }
public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string YearOfStudy { get; set; } = string.Empty;
    public string EmergencyContactNumber { get; set; } = string.Empty;    
    public string Department { get; set; } = string.Empty;
    public string RoomNumber { get; set; } = string.Empty;
    public UserStatus Status { get; set; }
}