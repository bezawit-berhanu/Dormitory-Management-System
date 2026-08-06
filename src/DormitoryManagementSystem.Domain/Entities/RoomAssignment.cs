namespace DormitoryManagementSystem.Domain.Entities;
public class RoomAssignment
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public int StudentId { get; set; }
    public DateTime AssignmentDate { get; set; }
};