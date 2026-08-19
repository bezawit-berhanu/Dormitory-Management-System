using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DormitoryManagementSystem.API.Controllers;

[ApiController]
[Route("api/password")]
public sealed class PasswordController : ControllerBase
{
    private readonly IPasswordResetService _passwordResetService;

    public PasswordController(IPasswordResetService passwordResetService)
    {
        _passwordResetService = passwordResetService;
    }

    [AllowAnonymous]
    [HttpPost("forgot")]
    public async Task<IActionResult> Forgot(
        ForgotPasswordDto dto,
        CancellationToken cancellationToken)
    {
        await _passwordResetService.RequestResetAsync(dto.Email, cancellationToken);
        return Ok(new
        {
            message = "If an account exists for that email, a password reset link has been sent."
        });
    }

    [AllowAnonymous]
    [HttpPost("reset")]
    public async Task<IActionResult> Reset(
        ResetPasswordDto dto,
        CancellationToken cancellationToken)
    {
        try
        {
            await _passwordResetService.ResetPasswordAsync(
                dto.Token,
                dto.NewPassword,
                dto.ConfirmPassword,
                cancellationToken);

            return Ok(new { message = "Your password has been reset. You can now sign in." });
        }
        catch (ArgumentException exception)
        {
            return BadRequest(new { message = exception.Message });
        }
    }
}
