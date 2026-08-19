using MockRegistrarAPI.Models;

namespace MockRegistrarAPI.Data;

public static class MockDepartmentData
{
    public static List<RegistrarDepartment> Departments { get; } = new()
    {
        new() { DepartmentId = 1, DepartmentName = "Computer Science" },
        new() { DepartmentId = 2, DepartmentName = "Information Systems" },
        new() { DepartmentId = 3, DepartmentName = "Software Engineering" },
        new() { DepartmentId = 4, DepartmentName = "Electrical Engineering" },
        new() { DepartmentId = 5, DepartmentName = "Civil Engineering" },
        new() { DepartmentId = 6, DepartmentName = "Mechanical Engineering" },
        new() { DepartmentId = 7, DepartmentName = "Accounting" },
        new() { DepartmentId = 8, DepartmentName = "Management" },
        new() { DepartmentId = 9, DepartmentName = "Economics" },
        new() { DepartmentId = 10, DepartmentName = "Law" }
    };
}