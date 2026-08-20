using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface ITransferService
{
    Task<IEnumerable<TransferDto>> GetAllAsync();

    Task<TransferDto?> GetByIdAsync(int id);

    Task<TransferDto> CreateAsync(TransferDto dto);

    Task<bool> UpdateAsync(int id, TransferDto dto);

    Task<bool> DeleteAsync(int id);
}