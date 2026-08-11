using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Domain.Interfaces;
public interface IQRCodeRepository
{
    Task<QRCode?> GetByStudentIdAsync(int studentId);
    Task<QRCode?> GetByValueAsync(string qrCodeValue);
    Task AddAsync(QRCode qrCode);
    Task UpdateAsync(QRCode qrCode);
    Task DeleteAsync(QRCode qrCode);
}