namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

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
}
=======
public class QRCode
{
    public int Id { get; set; }
    public string Code { get; set; } = string.Empty;
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
