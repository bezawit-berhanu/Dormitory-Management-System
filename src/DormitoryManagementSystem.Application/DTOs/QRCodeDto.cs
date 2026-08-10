using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Application.DTOs;
public class QRCodeDto
{
 public int QRCodeId { get; set; }

    public int SId { get; set; }

    public string QRCodeValue { get; set; } = string.Empty;

    public DateTime GeneratedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public UserStatus Status { get; set; } = UserStatus.Active;
}