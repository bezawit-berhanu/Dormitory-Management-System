using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IViolationService
{
    Task<IEnumerable<ViolationDto>> GetAllAsync();
    Task<ViolationDto?> GetByIdAsync(int id);
    Task<ViolationDto> CreateAsync(ViolationDto dto);
    Task<bool> UpdateAsync(int id, ViolationDto dto);
    Task<bool> DeleteAsync(int id);
}