namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

public class CheckIn
{
    public int CheckInId { get; set; }

    public int StudentId { get; set; }
    public Student? Student { get; set; }

    public int RoomAssignmentId { get; set; }
    public RoomAssignment? RoomAssignment { get; set; }

    public DateTime CheckInDate { get; set; }

    public int CheckedInByUserId { get; set; }
    public User? CheckedInByUser { get; set; }
}
=======
public class CheckIn
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
