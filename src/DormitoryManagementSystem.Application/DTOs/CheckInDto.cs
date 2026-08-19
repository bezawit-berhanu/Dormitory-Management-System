namespace DormitoryManagementSystem.Application.DTOs;

public class CheckInDto
{
    public int StudentId { get; set; }
    public int RoomAssignmentId { get; set; }
    public DateTime CheckInDate { get; set; }
    public int CheckedInByUserId { get; set; }
}

public class CheckInHistoryDto
{
    public int CheckInId { get; set; }
    public int StudentId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int RoomAssignmentId { get; set; }
    public string RoomNumber { get; set; } = string.Empty;
    public string DormitoryName { get; set; } = string.Empty;
    public DateTime CheckInDate { get; set; }
    public DateTime? CheckOutDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public string CheckedInBy { get; set; } = string.Empty;
}