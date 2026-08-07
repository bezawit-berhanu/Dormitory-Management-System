using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;


    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<User?> GetByEmailAsync(string email)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.Email == email);
    }


    public async Task<User?> GetByIdAsync(int id)
    {
        return await _context.Users
            .Include(u => u.Role)
            .FirstOrDefaultAsync(u => u.UserId == id);
    }


    public async Task AddAsync(User user)
    {
        await _context.Users.AddAsync(user);
    }


    public Task UpdateAsync(User user)
    {
        _context.Users.Update(user);
        return Task.CompletedTask;
    }

public async Task<IEnumerable<User>> GetAllAsync()
{
    return await _context.Users
        .Include(u => u.Role)
        .ToListAsync();
}
    public async Task DeleteAsync(int id)
{
    var user = await GetByIdAsync(id);

    if(user != null)
    {
        _context.Users.Remove(user);
    }
}


    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}