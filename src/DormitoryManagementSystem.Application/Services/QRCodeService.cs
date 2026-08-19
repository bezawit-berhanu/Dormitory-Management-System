using AutoMapper;
using DormitoryManagementSystem.Application.DTOs;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;

namespace DormitoryManagementSystem.Application.Services;
public class QRCodeService :IQRCodeService
{
    private readonly IQRCodeRepository _qrCodeRepository;
    private readonly IMapper _mapper;

    public QRCodeService(IQRCodeRepository qrCodeRepository, IMapper mapper)
    {
        _qrCodeRepository = qrCodeRepository;
        _mapper = mapper;
    }

    public async Task<QRCodeDto> GenerateQRCodeAsync(int studentId)
    {
        var qrCode = new QRCode
        {
            SId = studentId,
            QRCodeValue = Guid.NewGuid().ToString(),
            GeneratedDate = DateTime.UtcNow,
            ExpiryDate = DateTime.UtcNow.AddYears(1)
        };
        await _qrCodeRepository.AddAsync(qrCode);
        return _mapper.Map<QRCodeDto>(qrCode);
    }

    public async Task<QRCodeDto?> GetQRCodeAsync (int studentId)
    {
        var qrCode = await _qrCodeRepository.GetByStudentIdAsync(studentId);

        if(qrCode == null) 
        return null;

        return _mapper.Map<QRCodeDto>(qrCode);
    }

    public async Task<bool> ValidateQRCodeAsync (string qrCodeValue)
    {
        var qrCode = await _qrCodeRepository.GetByValueAsync(qrCodeValue);

           if(qrCode== null) return false;


        return qrCode.ExpiryDate > DateTime.UtcNow;

return qrCode.ExpirationDate > DateTime.UtcNow;

    }

}
