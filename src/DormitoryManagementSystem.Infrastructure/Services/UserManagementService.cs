using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Services;

public class UserManagementService : IUserManagementService
{
    private readonly IUserRepository _userRepository;


    public UserManagementService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }



    public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
    {
        // Temporary implementation
        // We will add GetAllAsync to repository later if needed
        return new List<UserDto>();
    }



    public async Task<UserDto?> GetUserByIdAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);

        if(user == null)
            return null;


        return MapToDto(user);
    }



    public async Task<bool> UpdateUserAsync(UserDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId);

        if(user == null)
            return false;


        user.FullName = dto.FullName;
        user.Email = dto.Email;
        user.PhoneNumber = dto.PhoneNumber;


        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();


        return true;
    }



    public async Task<bool> DeleteUserAsync(int id)
    {
        var user = await _userRepository.GetByIdAsync(id);


        if(user == null)
            return false;


        await _userRepository.DeleteAsync(user);
        await _userRepository.SaveChangesAsync();


        return true;
    }



    private static UserDto MapToDto(User user)
    {
        return new UserDto
        {
            UserId = user.UserId,
            FullName = user.FullName,
            Email = user.Email,
            PhoneNumber = user.PhoneNumber ?? string.Empty,

            Role = user.Role != null 
                ? user.Role.RoleName 
                : string.Empty,

            IsActive = true
        };
    }
}