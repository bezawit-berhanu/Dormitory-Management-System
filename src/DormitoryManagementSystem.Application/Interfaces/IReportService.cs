using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IReportService
{
    Task<ReportDto> GenerateReportAsync(string reportType);
}