namespace DormitoryManagementSystem.Domain.Entities;

public class Room
{
    public int RoomId { get; set; }

    public int FloorId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public string Status { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    // Navigation
    public Floor? Floor { get; set; }

    public ICollection<Bed> Beds { get; set; }
        = new List<Bed>();

    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();
}