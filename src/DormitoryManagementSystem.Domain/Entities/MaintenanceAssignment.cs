namespace DormitoryManagementSystem.Domain.Entities;
public class MaintenanceAssignment
{
    public int AssignmentId { get; set; }

    public int MaintenanceRequestId { get; set; }

    public int UserId { get; set; }

    public DateTime AssignedDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public MaintenanceRequest MaintenanceRequest { get; set; } = null!;

    public User User { get; set; } = null!;
};