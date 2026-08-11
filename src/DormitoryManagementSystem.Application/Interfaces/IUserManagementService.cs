using DormitoryManagementSystem.Application.DTOs;
namespace DormitoryManagementSystem.Application.Interfaces;
public interface IUserManagementService
{
    Task<IEnumerable<UserDto>> GetAllUsersAsync();
    Task<UserDto?> GetUserByIdAsync(int userId);
    Task<bool> UpdateUserAsync(UserDto userDto);
    Task<bool> DeleteUserAsync(int UserId);
}