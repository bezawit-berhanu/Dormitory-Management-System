using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Application.Services;

public class TransferService : ITransferService
{
    private readonly ITransferRepository _repository;

    public TransferService(ITransferRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<TransferDto>> GetAllAsync()
    {
        var transfers = await _repository.GetAllAsync();

        return transfers.Select(t => new TransferDto
        {
            SId = t.SId,
            Reason = t.Reason,
            Status = t.Status,
            RequestDate = t.RequestDate
        });
    }

    public async Task<TransferDto?> GetByIdAsync(int id)
    {
        var transfer = await _repository.GetByIdAsync(id);

        if (transfer == null)
            return null;

        return new TransferDto
        {
            SId = transfer.SId,
            Reason = transfer.Reason,
            Status = transfer.Status,
            RequestDate = transfer.RequestDate
        };
    }

    public async Task<TransferDto> CreateAsync(TransferDto dto)
    {
        var transfer = new RoomTransferRequest
        {
            SId = dto.SId,
            Reason = dto.Reason,
            Status = dto.Status,
            RequestDate = dto.RequestDate
        };

        await _repository.AddAsync(transfer);

        return new TransferDto
        {
            SId = transfer.SId,
            Reason = transfer.Reason,
            Status = transfer.Status,
            RequestDate = transfer.RequestDate
        };
    }

    public async Task<bool> UpdateAsync(int id, TransferDto dto)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            return false;

        existing.SId = dto.SId;
        existing.Reason = dto.Reason;
        existing.Status = dto.Status;
        existing.RequestDate = dto.RequestDate;

        await _repository.UpdateAsync(existing);

        return true;
    }

    public async Task<bool> DeleteAsync(int id)
    {
        var existing = await _repository.GetByIdAsync(id);

        if (existing == null)
            return false;

        await _repository.DeleteAsync(id);

        return true;
    }
}