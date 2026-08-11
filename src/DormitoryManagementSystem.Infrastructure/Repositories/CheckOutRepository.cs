using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class CheckOutRepository : ICheckOutRepository
{
    private readonly ApplicationDbContext _context;


    public CheckOutRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<IEnumerable<CheckOut>> GetAllAsync()
    {
        return await _context.CheckOuts
            .ToListAsync();
    }


    public async Task<CheckOut?> GetByIdAsync(int id)
    {
        return await _context.CheckOuts
            .FirstOrDefaultAsync(c => c.CheckOutId == id);
    }


    public async Task<IEnumerable<CheckOut>> GetByStudentIdAsync(int studentId)
    {
        return await _context.CheckOuts
            .Where(c => c.SId == studentId)
            .ToListAsync();
    }


    public async Task<CheckOut> AddAsync(CheckOut checkOut)
    {
        await _context.CheckOuts.AddAsync(checkOut);

        return checkOut;
    }


    public async Task UpdateAsync(CheckOut checkOut)
    {
        _context.CheckOuts.Update(checkOut);
    }


    public async Task DeleteAsync(int id)
    {
        var checkOut = await GetByIdAsync(id);

        if(checkOut != null)
        {
            _context.CheckOuts.Remove(checkOut);
        }
    }


    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}