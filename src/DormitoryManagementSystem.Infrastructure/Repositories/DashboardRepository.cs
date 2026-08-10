using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class DashboardRepository : IDashboardRepository
{
    private readonly ApplicationDbContext _context;

    public DashboardRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<DashboardDto> GetDashboardAsync()
    {
        var totalDormitories = await _context.Dormitories.CountAsync();

        var totalRooms = await _context.Rooms.CountAsync();

        var occupiedRooms = await _context.Rooms
            .CountAsync(r => r.Status == "Occupied");

        var availableRooms = await _context.Rooms
            .CountAsync(r => r.Status == "Available");

        var totalMaintenanceRequests =
            await _context.MaintenanceRequests.CountAsync();

        var pendingMaintenanceRequests =
            await _context.MaintenanceRequests
                .CountAsync(m => m.Status == "Pending");

        var totalIncidents =
            await _context.SecurityIncidents.CountAsync();

        return new DashboardDto
        {
            TotalDormitories = totalDormitories,
            TotalRooms = totalRooms,
            OccupiedRooms = occupiedRooms,
            AvailableRooms = availableRooms,
            TotalMaintenanceRequests = totalMaintenanceRequests,
            PendingMaintenanceRequests = pendingMaintenanceRequests,
            TotalIncidents = totalIncidents
        };
    }
}