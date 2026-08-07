namespace DormitoryManagementSystem.Application.DTOs;

public class BedDto
{
    public int BedId { get; set; }
    public string BedNumber { get; set; } = string.Empty;
    public int RoomId { get; set; }
    public string Status { get; set; } = string.Empty;
}