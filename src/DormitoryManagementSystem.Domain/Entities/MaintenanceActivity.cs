namespace DormitoryManagementSystem.Domain.Entities;
public class MaintenanceActivity
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateReported { get; set; }
};
