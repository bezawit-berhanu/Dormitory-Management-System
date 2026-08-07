using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Enums;
using DormitoryManagementSystem.Domain.Interfaces;
namespace DormitoryManagementSystem.Application.Services;

public class AuthenticationService : IAuthenticationService
{
    private readonly IUserRepository _userRepository;


    public AuthenticationService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }


    public async Task<UserDto> RegisterAsync(RegisterDto dto)
    {
        var existingUser = await _userRepository.GetByEmailAsync(dto.Email);;

        if (existingUser != null)
        {
            throw new Exception("User already exists");
        }


        var user = new User
        {
            FullName = dto.FullName,
            Email = dto.Email,
            PasswordHash = dto.Password,
            PhoneNumber = dto.PhoneNumber,
            RoleId = dto.RoleId,
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow
        };


        await _userRepository.AddAsync(user);
        await _userRepository.SaveChangesAsync();


        return MapToDto(user);
    }



    public async Task<UserDto> LoginAsync(LoginDto dto)
    {
        var user = await _userRepository.GetByEmailAsync(dto.UserName);


        if (user == null)
        {
            throw new Exception("Invalid username or password");
        }


        if (user.PasswordHash != dto.Password)
        {
            throw new Exception("Invalid username or password");
        }


        return MapToDto(user);
    }



    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            FullName = user.FullName,

            Role = user.Role != null
                ? user.Role.RoleName
                : string.Empty,

            Department = string.Empty,

            IsActive = user.Status == UserStatus.Active
        };
    }
}