using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IStaffAuthenticationService
{
    Task<AuthenticationResponseDto>
        RegisterAsync(RegisterStaffDto dto);

    Task<AuthenticationResponseDto>
        LoginAsync(StaffLoginDto dto);
}