namespace MockRegistrarAPI.Models;

public class RegistrarStaff
{
    public string EmployeeId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Campus { get; set; } = string.Empty;

    // Manager, Proctor, or Maintenance
    public string Role { get; set; } = string.Empty;

    // Only relevant for Proctors.
    // Example: Male Block 4, Female Block 2, etc.
    public string? AssignedBlock { get; set; }

    // 1 = Active
    // 2 = Inactive
    public int Status { get; set; }
}