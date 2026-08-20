namespace DormitoryManagementSystem.Domain.Entities;

public class Announcement
{
    public int AnnouncementId { get; set; }
    // Foreign Key
    public int CreatedBy { get; set; }
    public string Title { get; set; } = string.Empty;

    public string Message { get; set; } = string.Empty;

    public DateTime PublishedDate { get; set; }

    public DateTime ExpiryDate { get; set; }

    public string Status { get; set; } = string.Empty;

    // Navigation Property
    public User CreatedByUser { get; set; } = null!;
};

