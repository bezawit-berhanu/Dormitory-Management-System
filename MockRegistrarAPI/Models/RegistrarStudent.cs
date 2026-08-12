namespace MockRegistrarAPI.Models;

public class RegistrarStudent
{
    public string StudentId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public string Department { get; set; } = string.Empty;

    public string Gender { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public int YearOfStudy { get; set; }

    // 1 = Active
    // 2 = Inactive
    // 3 = Graduated
    // 4 = Suspended
    public int Status { get; set; }
}