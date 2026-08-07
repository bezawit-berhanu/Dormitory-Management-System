using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.Application.Services;

public class ReportService : IReportService
{
    public async Task<ReportDto> GenerateReportAsync(string reportType)
    {
        var report = new ReportDto
        {
            ReportType = reportType,
            GeneratedDate = DateTime.Now,
            TotalCount = 0,
            Description = $"{reportType} report generated successfully."
        };

        return await Task.FromResult(report);
    }
}