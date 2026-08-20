using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class DepartmentRepository : IDepartmentRepository
{
    private readonly ApplicationDbContext _context;

    public DepartmentRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<Department?> GetByRegistrarIdAsync(int departmentId)
    {
        return await _context.Departments
            .FirstOrDefaultAsync(d => d.DepartmentId == departmentId);
    }

    public async Task AddAsync(Department department)
    {
        await _context.Departments.AddAsync(department);
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}