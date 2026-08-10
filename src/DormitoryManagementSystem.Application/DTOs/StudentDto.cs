using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Application.DTOs;

public class StudentDto {
    public int SId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Gender {get; set;} = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string EmergencyContact { get; set; } = string.Empty;
    public string YearOfStudy { get; set; } =string.Empty;
    public UserStatus Status { get; set; }
}