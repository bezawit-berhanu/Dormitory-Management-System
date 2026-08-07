namespace DormitoryManagementSystem.Domain.Entities;

public class CheckIn
{
    public int CheckInId { get; set; }


    public int SId { get; set; }


    public int RoomAssignmentId { get; set; }


    public DateTime CheckInDate { get; set; }


    public int CheckedInBy { get; set; }



    // Navigation

    public Student? Student { get; set; }


    public RoomAssignment? RoomAssignment { get; set; }


    public User? CheckedInByUser { get; set; }
}