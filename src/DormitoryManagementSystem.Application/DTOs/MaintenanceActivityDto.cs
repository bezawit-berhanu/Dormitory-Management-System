namespace DormitoryManagementSystem.Application.DTOs;

public class MaintenanceActivityDto
{
    public int MaintenanceRequestId { get; set; }

    public int PerformedBy { get; set; }

    public string ActivityDescription { get; set; } = string.Empty;

    public DateTime ActivityDate { get; set; }

    public string Status { get; set; } = string.Empty;
}