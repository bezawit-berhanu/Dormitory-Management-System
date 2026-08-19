using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class RoomAssignment
{
    public int RoomAssignmentId { get; set; }

    // Student assigned to the room
    public int SId { get; set; }
    public Student? Student { get; set; }
    public string StudentId {get; set;} = string.Empty;
    public int RoomId { get; set; }
    public Room? Room { get; set; }

    public int BedId { get; set; }
    public Bed? Bed { get; set; }

    public DateTime AssignedDate { get; set; }

    public int AssignedByUserId { get; set; }
    public User? AssignedByUser { get; set; }
    public string Status { get; set; } = string.Empty;

};
