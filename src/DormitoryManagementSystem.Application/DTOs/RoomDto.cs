namespace DormitoryManagementSystem.Application.DTOs;

public class RoomDto
{
    public int RoomId { get; set; }

    public string RoomNumber { get; set; } = string.Empty;

    public int FloorId { get; set; }

    public string Capacity { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}