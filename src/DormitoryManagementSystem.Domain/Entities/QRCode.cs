namespace DormitoryManagementSystem.Domain.Entities;
public class QRCode
{
    public int QRCodeId { get; set; }

    // Foreign Key: Student
    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public string QRCodeValue { get; set; } = string.Empty;

    public DateTime GeneratedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Status { get; set; } = string.Empty;
};

