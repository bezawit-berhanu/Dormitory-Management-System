namespace DormitoryManagementSystem.Domain.Entities;
public class Room
{
    public int Id { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int BlockId { get; set; }
};