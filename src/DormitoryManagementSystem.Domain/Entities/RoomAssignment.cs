using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class RoomAssignment
{
    public int RoomAssignmentId { get; set; }

    public int SId { get; set; }

    public int RoomId { get; set; }

    public int BedId { get; set; }

    public DateTime AssignedDate { get; set; }

    public int AssignedByUserId { get; set; }

    public UserStatus Status { get; set; }


    // Navigation

    public Student? Student { get; set; }

    public Room? Room { get; set; }

    public Bed? Bed { get; set; }

    public User? AssignedByUser { get; set; }
}