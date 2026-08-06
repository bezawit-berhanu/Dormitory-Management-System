namespace DormitoryManagementSystem.Domain.Entities;
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