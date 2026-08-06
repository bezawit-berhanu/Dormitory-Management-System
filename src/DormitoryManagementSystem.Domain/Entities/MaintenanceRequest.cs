namespace DormitoryManagementSystem.Domain.Entities;
public class MaintenanceRequest
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateRequested { get; set; }
};