namespace DormitoryManagementSystem.Application.DTOs;

public class ReportDto
{
    public string ReportType { get; set; } = string.Empty;

    public DateTime GeneratedDate { get; set; }

    public int TotalCount { get; set; }

    public string Description { get; set; } = string.Empty;
}