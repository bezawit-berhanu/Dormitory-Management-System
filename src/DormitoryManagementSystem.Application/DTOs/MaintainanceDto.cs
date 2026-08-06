namespace DormitoryManagementSystem.Application.DTOs;

public class MaintenanceDto
{
    public int MaintenanceRequestId { get; set; }

    public int RoomId { get; set; }

    public int RequestedBy { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; }

    public string Status { get; set; } = string.Empty;


    // Maintenance Assignment
    public int? AssignedUserId { get; set; }

    public DateTime? AssignedDate { get; set; }

    public string? AssignmentStatus { get; set; }
}