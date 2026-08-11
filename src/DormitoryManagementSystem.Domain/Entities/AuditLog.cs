namespace DormitoryManagementSystem.Domain.Entities;

public class AuditLog
{
    public int AuditLogId { get; set; }   // Primary Key

    public string Action { get; set; } = string.Empty;

    public string UserName { get; set; } = string.Empty;

    public DateTime Date { get; set; }
}
