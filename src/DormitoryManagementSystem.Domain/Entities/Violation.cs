namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

public class Violation
{
    public int ViolationId { get; set; }


    // Foreign Key: Student
    public int StudentId { get; set; }
    public Student? Student { get; set; }


    // Foreign Key: User who recorded the violation
    public int RecordedByUser { get; set; }
    public User? RecordedByUserId { get; set; }


    public string ViolationType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ViolationDate { get; set; }

    public string Penalty { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}
=======
public class Violation
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateOccurred { get; set; }
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
