namespace DormitoryManagementSystem.Application.DTOs;

public class MaintenanceAssignmentDto
{
    public int MaintenanceRequestId { get; set; }

    public int UserId { get; set; }

    public DateTime AssignedDate { get; set; }

    public string Status { get; set; } = string.Empty;
}