using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class Bed
{
    public int BedId { get; set; }

    public int RoomId { get; set; }
    public string Status { get; set; } = string.Empty;
    public string BedNumber { get; set; } = string.Empty;

    // Navigation
    public Room? Room { get; set; }

    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();
}
