using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class Room
{
    public int RoomId { get; set; }

    public int FloorId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int Capacity { get; set; }

    public UserStatus Status { get; set; }


    // Navigation
    public ICollection<Bed> Beds { get; set; } = new List<Bed>();

    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();
        public Floor? Floor { get; set; }
}