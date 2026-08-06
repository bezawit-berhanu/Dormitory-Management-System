namespace DormitoryManagementSystem.Domain.Entities;
public class SecurityIncident
{
    public int IncidentId { get; set; }

    public int RoomId { get; set; }

    public int ReportedBy { get; set; }

    public string IncidentType { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime IncidentDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public Room Room { get; set; } = null!;

    public User ReportedByUser { get; set; } = null!;
};