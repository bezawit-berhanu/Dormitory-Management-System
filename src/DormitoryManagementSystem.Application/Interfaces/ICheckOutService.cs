using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface ICheckOutService
{
    Task<IEnumerable<CheckOutDto>> GetCheckOutHistoryAsync(int studentId);

    Task<CheckOutDto> CheckOutStudentAsync(CheckOutDto dto);

    Task<CheckOutDto?> GetCheckOutByIdAsync(int id);

    Task<bool> UpdateCheckOutAsync(int id, CheckOutDto dto);

    Task<bool> DeleteCheckOutAsync(int id);
}