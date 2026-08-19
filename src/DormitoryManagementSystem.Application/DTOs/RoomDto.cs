namespace DormitoryManagementSystem.Application.DTOs;

public class RoomDto
{
    public int RoomId { get; set; }
    public int FloorId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public int Capacity { get; set; }
    public int AvailableBeds { get; set; }
}
//Room dto