using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class DashboardService : IDashboardService
{
    public async Task<DashboardDto> GetDashboardAsync()
    {
        var dashboard = new DashboardDto
        {
            TotalDormitories = 0,
            TotalRooms = 0,
            OccupiedRooms = 0,
            AvailableRooms = 0,
            TotalMaintenanceRequests = 0,
            PendingMaintenanceRequests = 0,
            TotalIncidents = 0
        };

        return await Task.FromResult(dashboard);
    }
}