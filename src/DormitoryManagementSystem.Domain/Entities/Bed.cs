namespace DormitoryManagementSystem.Domain.Entities;

public class Bed
{
    public int BedId { get; set; }

    public string BedNumber { get; set; } = string.Empty;

    public int RoomId { get; set; }

    public string Status { get; set; } = string.Empty;

    public Room Room { get; set; } = null!;

    public bool IsActive { get; set; } = true;
}