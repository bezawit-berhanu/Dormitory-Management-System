using MockRegistrarAPI.Models;

namespace MockRegistrarAPI.Data;

public static class MockStudentData
{
    public static List<RegistrarStudent> Students { get; } =
        GenerateStudents();

    private static List<RegistrarStudent> GenerateStudents()
    {
        var firstNames = new[]
        {
            "Abebe", "Hana", "Meron", "Dawit", "Sara",
            "Betelhem", "Samuel", "Rahel", "Nahom", "Mekdes",
            "Yonas", "Liya", "Eyob", "Selam", "Bereket",
            "Ruth", "Daniel", "Eden", "Yonatan", "Mimi"
        };

        var lastNames = new[]
        {
            "Kebede", "Tesfaye", "Mekonnen", "Alemu", "Berhanu",
            "Gebre", "Tadesse", "Haile", "Abebe", "Getachew",
            "Worku", "Bekele", "Girma", "Lemma", "Assefa"
        };

        var departments = new[]
        {
            new { Id = 1, Name = "Computer Science" },
            new { Id = 2, Name = "Information Systems" },
            new { Id = 3, Name = "Software Engineering" },
            new { Id = 4, Name = "Electrical Engineering" },
            new { Id = 5, Name = "Civil Engineering" },
            new { Id = 6, Name = "Mechanical Engineering" },
            new { Id = 7, Name = "Accounting" },
            new { Id = 8, Name = "Management" },
            new { Id = 9, Name = "Economics" },
            new { Id = 10, Name = "Law" }
        };

        var students = new List<RegistrarStudent>();

        for (int i = 1; i <= 150; i++)
        {
            var firstName = firstNames[(i - 1) % firstNames.Length];
            var lastName = lastNames[(i * 3) % lastNames.Length];

            var department = departments[(i - 1) % departments.Length];

            // Create different statuses so the dashboard
            // has realistic test data.
            var status = i % 20 == 0
                ? 4                         // Suspended
                : i % 15 == 0
                    ? 3                     // Graduated
                    : i % 10 == 0
                        ? 2                 // Inactive
                        : 1;                // Active

            students.Add(new RegistrarStudent
            {
                StudentId = $"AAU2024{i:0000}",

                FullName = $"{firstName} {lastName}",

                DepartmentId = department.Id,

                Department = department.Name,

                Gender = i % 2 == 0 ? "Female" : "Male",

                DateOfBirth =
                    new DateTime(2001 + (i % 6), (i % 12) + 1, (i % 27) + 1),

                YearOfStudy = (i % 4) + 1,

                Status = status
            });
        }

        return students;
    }
}