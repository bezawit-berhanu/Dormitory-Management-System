using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class TransferRepository : ITransferRepository
{
    private readonly ApplicationDbContext _context;
    public TransferRepository(ApplicationDbContext context)
    {
        _context = context;
    }
    public Task<IEnumerable<RoomTransferRequest>> GetAllAsync()
    {
        return Task.FromResult(_context.RoomTransferRequests.AsEnumerable());
    }

    public Task<RoomTransferRequest?> GetByIdAsync(int id)
    {
        return Task.FromResult(
            _context.RoomTransferRequests.FirstOrDefault(t => t.TransferRequestId == id)
        );
    }

    public Task AddAsync(RoomTransferRequest transfer)
    {
        _context.RoomTransferRequests.Add(transfer);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(RoomTransferRequest transfer)
    {
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var item = _context.RoomTransferRequests.FirstOrDefault(x => x.TransferRequestId == id);

        if (item != null)
            _context.RoomTransferRequests.Remove(item);

        return Task.CompletedTask;
    }
}