namespace DormitoryManagementSystem.Domain.Entities;
public class MaintenanceActivity
{
    public int ActivityId { get; set; }

    public int MaintenanceRequestId { get; set; }

    public int PerformedBy { get; set; }

    public string ActivityDescription { get; set; } = string.Empty;

    public DateTime ActivityDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public MaintenanceRequest MaintenanceRequest { get; set; } = null!;

    public User PerformedByUser { get; set; } = null!;
};
