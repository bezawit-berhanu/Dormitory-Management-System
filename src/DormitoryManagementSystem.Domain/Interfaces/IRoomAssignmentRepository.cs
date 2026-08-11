using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;

public interface IRoomAssignmentRepository
{
    Task<IEnumerable<RoomAssignment>> GetAllAsync();

    Task<RoomAssignment?> GetByIdAsync(int id);

    Task<IEnumerable<RoomAssignment>> GetByStudentIdAsync(int studentId);

    Task AddAsync(RoomAssignment assignment);

    Task UpdateAsync(RoomAssignment assignment);

    Task DeleteAsync(RoomAssignment assignment);

    Task<bool> ExistsAsync(int id);
}