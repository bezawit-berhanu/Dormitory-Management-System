using Microsoft.AspNetCore.Mvc;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;

namespace DormitoryManagementSystem.API.Controllers;


[ApiController]
[Route("api/[controller]")]
public class AuthenticationController : ControllerBase
{
    private readonly IAuthenticationService _authenticationService;


    public AuthenticationController(IAuthenticationService authenticationService)
    {
        _authenticationService = authenticationService;
    }



    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterDto dto)
    {
        try
        {
            var result = await _authenticationService.RegisterAsync(dto);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }



    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginDto dto)
    {
        try
        {
            var result = await _authenticationService.LoginAsync(dto);

            return Ok(result);
        }
        catch(Exception ex)
        {
            return Unauthorized(ex.Message);
        }
    }
}