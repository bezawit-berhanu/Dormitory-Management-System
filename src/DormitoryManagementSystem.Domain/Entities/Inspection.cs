namespace DormitoryManagementSystem.Domain.Entities;
public class Inspection
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateTime InspectionDate { get; set; }
    public string InspectorName { get; set; } = string.Empty;
};
