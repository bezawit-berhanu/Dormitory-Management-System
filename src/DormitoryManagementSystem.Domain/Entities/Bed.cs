namespace DormitoryManagementSystem.Domain.Entities;
public class Bed
{
    public int Id { get; set; }
    public string BedNumber { get; set; } = string.Empty;
    public int RoomId { get; set; }
};
