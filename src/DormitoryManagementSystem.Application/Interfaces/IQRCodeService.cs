using DormitoryManagementSystem.Application.DTOs;

namespace DormitoryManagementSystem.Application.Interfaces;

public interface IQRCodeService
{
    Task<QRCodeDto> GenerateQRCodeAsync(int studentId);

    Task<QRCodeDto?> GetQRCodeAsync(int studentId);

    Task<bool> ValidateQRCodeAsync(string qrCodeValue);
}