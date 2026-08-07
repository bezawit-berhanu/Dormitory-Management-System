namespace DormitoryManagementSystem.Application.DTOs;
public class CheckOutDto
{
    public int CheckOutId { get; set; }
    public int RoomAssignmentId { get; set; }
    public DateTime CheckOutDate { get; set; }
    public string Reason { get; set; } = string.Empty;
}