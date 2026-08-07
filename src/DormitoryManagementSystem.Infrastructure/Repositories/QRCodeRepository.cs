using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;

namespace DormitoryManagementSystem.Infrastructure.Repositories;

public class QRCodeRepository : IQRCodeRepository
{
    private readonly ApplicationDbContext _context;

    public QRCodeRepository(ApplicationDbContext context)
    {
        _context = context;
    }


    public async Task<QRCode?> GetByStudentIdAsync(int studentId)
    {
        return await _context.QRCodes
            .FirstOrDefaultAsync(q => q.SId == studentId);
    }


    public async Task<QRCode?> GetByValueAsync(string qrCodeValue)
    {
        return await _context.QRCodes
            .FirstOrDefaultAsync(q => q.QRCodeValue == qrCodeValue);
    }


    public async Task AddAsync(QRCode qrCode)
    {
        await _context.QRCodes.AddAsync(qrCode);
        await _context.SaveChangesAsync();
    }


    public async Task UpdateAsync(QRCode qrCode)
    {
        _context.QRCodes.Update(qrCode);
        await _context.SaveChangesAsync();
    }


    public async Task DeleteAsync(QRCode qrCode)
    {
        _context.QRCodes.Remove(qrCode);
        await _context.SaveChangesAsync();
    }
}