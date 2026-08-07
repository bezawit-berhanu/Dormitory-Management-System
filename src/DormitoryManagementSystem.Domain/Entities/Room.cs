namespace DormitoryManagementSystem.Domain.Entities;
public class Room
{
    public int RoomId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int FloorId { get; set; } //fk
    public Floor Floor { get; set; } = null!;
    public string Capacity { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    };