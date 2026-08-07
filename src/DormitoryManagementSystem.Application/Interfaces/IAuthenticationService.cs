using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IAuthenticationService
{
    Task<UserDto> RegisterAsync(RegisterDto dto);

    Task<UserDto> LoginAsync(LoginDto dto);
}