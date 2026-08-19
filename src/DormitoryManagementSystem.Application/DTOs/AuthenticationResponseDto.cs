namespace DormitoryManagementSystem.Application.DTOs;

public class AuthenticationResponseDto
{
    public string Token { get; set; } = string.Empty;

    public UserDto User { get; set; } = null!;
}