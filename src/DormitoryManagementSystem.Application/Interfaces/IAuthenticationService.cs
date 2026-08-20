using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IAuthenticationService
{
    Task<AuthenticationResponseDto>
        RegisterAsync(RegisterDto dto);

    Task<AuthenticationResponseDto>
        LoginAsync(LoginDto dto);
}