using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;


namespace DormitoryManagementSystem.Infrastructure.Repositories;


public class CheckInRepository : ICheckInRepository
{

    private readonly ApplicationDbContext _context;


    public CheckInRepository(ApplicationDbContext context)
    {
        _context = context;
    }



    public async Task AddAsync(CheckIn checkIn)
    {
        await _context.CheckIns.AddAsync(checkIn);
    }



    public async Task<IEnumerable<CheckIn>> GetHistoryAsync(int studentId)
    {
        return await _context.CheckIns
            .Where(c => c.SId == studentId)
            .ToListAsync();
    }



    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}