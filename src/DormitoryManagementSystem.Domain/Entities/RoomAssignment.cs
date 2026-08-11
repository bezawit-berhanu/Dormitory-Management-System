using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class RoomAssignment
{
    public int RoomAssignmentId { get; set; }

    // Student assigned to the room
    public int SId { get; set; }
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

};
