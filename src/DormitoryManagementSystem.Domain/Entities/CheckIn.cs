namespace DormitoryManagementSystem.Domain.Entities;

public class CheckIn
{
    public int CheckInId { get; set; }

    public int Id { get; set; }
    public Student? Student { get; set; }

    public int RoomAssignmentId { get; set; }
    public RoomAssignment? RoomAssignment { get; set; }

    public DateTime CheckInDate { get; set; }

    public int CheckedInByUserId { get; set; }
    public User? CheckedInByUser { get; set; }
};
