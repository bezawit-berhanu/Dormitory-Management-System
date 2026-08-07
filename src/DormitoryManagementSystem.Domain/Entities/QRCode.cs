using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;
public class QRCode
{
    public int QRCodeId { get; set; }
    public int SId { get; set; }
    public string QRCodeValue { get; set; } = string.Empty;
    public DateTime GeneratedDate { get; set; }
    public DateTime ExpirationDate { get; set; }
    public UserStatus Status {get;set;}

     public Student? Student { get; set; }

};