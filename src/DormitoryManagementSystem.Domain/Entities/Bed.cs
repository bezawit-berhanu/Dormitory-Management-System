using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class Bed
{
    public int BedId { get; set; }

    public int RoomId { get; set; }

    public string BedNumber { get; set; } = string.Empty;

    public UserStatus Status { get; set; }


    // Navigation
    public Room? Room { get; set; }

    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();
}