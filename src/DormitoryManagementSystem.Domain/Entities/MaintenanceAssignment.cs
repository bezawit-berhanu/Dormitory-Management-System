namespace DormitoryManagementSystem.Domain.Entities;
public class MaintenanceAssignment
{
    public int Id { get; set; }
    public int MaintenanceActivityId { get; set; }
    public int StaffId { get; set; }
    public DateTime AssignmentDate { get; set; }
};