namespace DormitoryManagementSystem.Application.DTOs;
public class CreateStudentDto {
    public int UserId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string StudentId { get; set; } = string.Empty;
    public int DepartmentId { get; set; }
    public string Gender {get; set;} = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string YearOfStudy { get; set; } =string.Empty;
    public string EmergencyContactNumber { get; set; } = string.Empty;
};