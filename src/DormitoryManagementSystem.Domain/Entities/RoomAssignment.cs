namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

public class RoomAssignment
{
    public int RoomAssignmentId { get; set; }

    // Student assigned to the room
    public int StudentId { get; set; }
    public Student? Student { get; set; }

    // Assigned room
    public int RoomId { get; set; }
    public Room? Room { get; set; }

    // Assigned bed
    public int BedId { get; set; }
    public Bed? Bed { get; set; }

    // Date of assignment
    public DateTime AssignedDate { get; set; }

    // Staff/Admin who assigned the room
    public int AssignedByUserId { get; set; }
    public User? AssignedByUser { get; set; }

    // Active, Transferred, CheckedOut, Cancelled, etc.
    public string Status { get; set; } = string.Empty;
}
=======
public class RoomAssignment
{
    public int RoomAssignmentId { get; set; }
    public int StudentId { get; set; }
    public Student Student { get; set; } = null!;
    public int BedId { get; set; }
    public Bed Bed { get; set; } = null!;
    public int RoomId { get; set; }
    public Room Room { get; set; } = null!;
    public DateTime AssignedDate { get; set; }
    public int AssignedByUserId { get; set; }
    public User AssignedByUser { get; set; } = null!;
    public string Status { get; set; } = null!;
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
