using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface ICheckInRepository
{
    Task AddAsync(CheckIn checkIn);

    Task<IEnumerable<CheckIn>> GetHistoryAsync(int studentId);

    Task SaveAsync();
}