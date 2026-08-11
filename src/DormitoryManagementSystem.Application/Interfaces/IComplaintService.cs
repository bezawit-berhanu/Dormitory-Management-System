using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IComplaintService
{

    Task<IEnumerable<ComplaintDto>> GetAllAsync();
    Task<ComplaintDto> CreateAsync(ComplaintDto dto);
    Task<bool> UpdateAsync(int id, ComplaintDto dto);
    Task<ComplaintDto?> GetByIdAsync(int id);
    Task<bool> DeleteAsync(int id);
}