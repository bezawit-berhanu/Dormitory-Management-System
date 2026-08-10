using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Application.DTOs;
public class RoomAssignmentDto
{
    public int RoomId { get; set; }
    public string RoomAssignmentId { get; set; } = string.Empty;
    public int BedId { get; set; }
    public string StudentName { get; set; } = string.Empty;
    public int StudentId { get; set; }
    public DateTime AssignedDate { get; set; }
    public int AssignedByUserId { get; set; }
    public UserStatus Status { get; set; }
}