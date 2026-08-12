namespace DormitoryManagementSystem.Application.DTOs;

public class RegisterDto
{
    public string StudentId { get; set; } = string.Empty;

    public string FullName { get; set; } = string.Empty;

    public string Email { get; set; } = string.Empty;

    public string PhoneNumber { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
    
    public int RoleId { get; set; }
}