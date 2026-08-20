using DormitoryManagementSystem.Application.DTOs;
namespace DormitoryManagementSystem.Application.Interfaces;
public interface ICheckInService
{
    Task<IEnumerable<CheckInDto>> GetCheckInHistoryAsync(int studentId);
    Task<CheckInDto> CheckInStudentAsync(CheckInDto dto);
}