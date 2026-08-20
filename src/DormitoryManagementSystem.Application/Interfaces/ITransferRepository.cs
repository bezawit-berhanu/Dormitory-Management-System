using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface ITransferRepository
{
    Task<IEnumerable<RoomTransferRequest>> GetAllAsync();

    Task<RoomTransferRequest?> GetByIdAsync(int id);

    Task AddAsync(RoomTransferRequest transfer);

    Task UpdateAsync(RoomTransferRequest transfer);

    Task DeleteAsync(int id);
}