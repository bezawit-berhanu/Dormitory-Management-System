namespace DormitoryManagementSystem.Domain.Entities;
public class Student
{
    public int StudentId { get; set; }
    public int UserId { get; set; }
     public string Name { get; set; } = string.Empty;
    public string StudentNumber { get; set; } = string.Empty;
     public int DepartmentId { get; set; }
    public string Gender { get; set; } = string.Empty;
    public DateTime DateOfBirth { get; set; }
    public string YearOfStudy { get; set; } = string.Empty;
    public string EmergencyContactNumber { get; set; } = string.Empty;
public string Status { get; set; } = string.Empty;
};