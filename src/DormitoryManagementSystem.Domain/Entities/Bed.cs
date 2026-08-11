using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class Bed
{
    public int BedId { get; set; }

    public int RoomId { get; set; }
<<<<<<< HEAD
    public string Status { get; set; } = string.Empty;
    public Room Room { get; set; } = null!;

};
=======

    public string BedNumber { get; set; } = string.Empty;

    public UserStatus Status { get; set; }


    // Navigation
    public Room? Room { get; set; }

    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();
}
>>>>>>> origin
