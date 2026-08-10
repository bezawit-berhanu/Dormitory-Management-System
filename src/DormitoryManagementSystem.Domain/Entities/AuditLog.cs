namespace DormitoryManagementSystem.Domain.Entities;

public class AuditLog {
    public int AuditLogId { get; set; }

    public int UserId { get; set; }

    public string Action { get; set; } = string.Empty;

    public string TableName { get; set; } = string.Empty;

    public int RecordId { get; set; }

    public DateTime ActionDate { get; set; }

    public User User { get; set; } = null!;
}