using DormitoryManagementSystem.Domain.Enums;

namespace DormitoryManagementSystem.Application.DTOs;

public class RoomAssignmentDto
{

    public int RoomAssignmentId { get; set; }

    public string StudentId { get; set; }= string.Empty;

    public int RoomId { get; set; }
    public int BedId { get; set; }
    public string StudentName { get; set; } = string.Empty;

    public DateTime AssignedDate { get; set; }

    public int AssignedByUserId { get; set; }

    public string Status { get; set; } = string.Empty;
}
