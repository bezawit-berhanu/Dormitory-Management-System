namespace DormitoryManagementSystem.Domain.Entities;
public class MaintenanceRequest
{
    public int MaintenanceRequestId { get; set; }

    public int RoomId { get; set; }

    public int RequestedBy { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public string Priority { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public Room Room { get; set; } = null!;

    public User RequestedByUser { get; set; } = null!;
};