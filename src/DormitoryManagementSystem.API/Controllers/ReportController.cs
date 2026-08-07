using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReportController : ControllerBase
{
    private readonly IReportService _service;

    public ReportController(IReportService service)
    {
        _service = service;
    }

    [HttpGet("{reportType}")]
    public async Task<IActionResult> GenerateReport(string reportType)
    {
        var report = await _service.GenerateReportAsync(reportType);

        return Ok(report);
    }
}