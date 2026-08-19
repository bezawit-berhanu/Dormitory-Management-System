using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/staff-authentication")]
public class StaffAuthenticationController : ControllerBase
{
    private readonly IStaffAuthenticationService
        _staffAuthenticationService;

    public StaffAuthenticationController(
        IStaffAuthenticationService staffAuthenticationService)
    {
        _staffAuthenticationService =
            staffAuthenticationService;
    }


    [HttpPost("register")]
    public async Task<IActionResult> Register(
        [FromBody] RegisterStaffDto dto)
    {
        try
        {
            var result =
                await _staffAuthenticationService
                    .RegisterAsync(dto);

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(new
            {
                message = ex.Message
            });
        }
    }


    [HttpPost("login")]
    public async Task<IActionResult> Login(
        [FromBody] StaffLoginDto dto)
    {
        try
        {
            var result =
                await _staffAuthenticationService
                    .LoginAsync(dto);

            return Ok(new
            {
                token = result.Token,
                user = result.User
            });
        }
        catch (Exception ex)
        {
            return Unauthorized(new
            {
                message = ex.Message
            });
        }
    }
}