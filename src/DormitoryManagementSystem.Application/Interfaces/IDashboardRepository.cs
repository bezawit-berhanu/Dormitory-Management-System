using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IDashboardRepository
{
    Task<DashboardDto> GetDashboardAsync();
}