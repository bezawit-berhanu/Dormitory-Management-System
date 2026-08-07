namespace DormitoryManagementSystem.Application.DTOs;

public class DashboardDto
{
    public int TotalDormitories { get; set; }

    public int TotalRooms { get; set; }

    public int OccupiedRooms { get; set; }

    public int AvailableRooms { get; set; }

    public int TotalMaintenanceRequests { get; set; }

    public int PendingMaintenanceRequests { get; set; }

    public int TotalIncidents { get; set; }
}