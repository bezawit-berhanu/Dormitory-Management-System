namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

public class CheckOut
{
    public int CheckOutId { get; set; }

    // Foreign Key: Student
    public int StudentId { get; set; }
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
=======
public class CheckOut
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckOutDate { get; set; }
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
