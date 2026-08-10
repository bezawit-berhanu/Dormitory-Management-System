namespace DormitoryManagementSystem.Domain.Entities;
public class Violation
{
    public int ViolationId { get; set; }


    // Foreign Key: Student
    public int SId { get; set; }
    public Student? Student { get; set; }


    // Foreign Key: User who recorded the violation
    public int RecordedByUser { get; set; }
    public User? RecordedByUserId { get; set; }


    public string ViolationType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ViolationDate { get; set; }

    public string Penalty { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

};
