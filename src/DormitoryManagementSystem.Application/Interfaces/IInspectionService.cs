using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IInspectionService
{
    Task<IEnumerable<InspectionDto>> GetAllAsync();

    Task<InspectionDto?> GetByIdAsync(int id);

    Task<InspectionDto> CreateAsync(InspectionDto dto);

    Task<bool> UpdateAsync(int id, InspectionDto dto);

    Task DeleteAsync(int id);
}