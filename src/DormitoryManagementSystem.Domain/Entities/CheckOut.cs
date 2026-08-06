namespace DormitoryManagementSystem.Domain.Entities;

public class CheckOut
{
    public int CheckOutId { get; set; }

    // Foreign Key: Student
    public int Id { get; set; }
    public Student? Student { get; set; }

    // Foreign Key: RoomAssignment
    public int RoomAssignmentId { get; set; }
    public RoomAssignment? RoomAssignment { get; set; }

    public DateTime CheckOutDate { get; set; }

    // Foreign Key: User who checked out student
    public int CheckedOutBy { get; set; }
    public User? CheckedOutByUser { get; set; }

    public string Reason { get; set; } = string.Empty;
}
