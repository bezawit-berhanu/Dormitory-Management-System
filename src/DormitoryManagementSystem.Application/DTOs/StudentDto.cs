using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Application.DTOs;

public class StudentDto {
    public int SId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public int DepratmentId {get; set;}
    public string DepartmentName { get; set; } = string.Empty;
    public string Gender {get; set;} = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string EmergencyContactNumber { get; set; } = string.Empty;
    public int YearOfStudy { get; set; }
    public StudentStatus Status { get; set; }
}