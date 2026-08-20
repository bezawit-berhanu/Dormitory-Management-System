using Microsoft.AspNetCore.Mvc;
using MockRegistrarAPI.Data;

namespace MockRegistrarAPI.Controllers;

[ApiController]
[Route("api/staff")]
public class StaffController : ControllerBase
{
    [HttpGet("{employeeId}")]
    public IActionResult GetStaffByEmployeeId(
        string employeeId)
    {
        var staff = MockStaffData.Staff
            .FirstOrDefault(
                s => s.EmployeeId.Equals(
                    employeeId,
                    StringComparison.OrdinalIgnoreCase));

        if (staff == null)
        {
            return NotFound();
        }

        return Ok(staff);
    }
}