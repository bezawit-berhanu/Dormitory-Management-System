namespace DormitoryManagementSystem.Application.DTOs;

public class AnnouncementDto
{
    public int AnnouncementId { get; set; }

    public int CreatedBy { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Status { get; set; } = string.Empty;
}