using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class ViolationRepository : IViolationRepository
{
    private readonly ApplicationDbContext _context;
    public ViolationRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public Task<IEnumerable<Violation>> GetAllAsync()
    {
        return Task.FromResult(_context.Violations.AsEnumerable());
    }

    public Task<Violation?> GetByIdAsync(int id)
    {
        return Task.FromResult(
            _context.Violations.FirstOrDefault(v => v.ViolationId == id)
        );
    }

    public Task AddAsync(Violation violation)
    {
        _context.Violations.Add(violation);
        return Task.CompletedTask;
    }

    public Task UpdateAsync(Violation violation)
    {
        _context.Violations.Update(violation);
        return Task.CompletedTask;
    }

    public Task DeleteAsync(int id)
    {
        var item = _context.Violations.FirstOrDefault(x => x.ViolationId == id);

        if (item != null)
            _context.Violations.Remove(item);

        return Task.CompletedTask;
    }
}