namespace DormitoryManagementSystem.Domain.Entities;

public class CheckOut
{
    public int CheckOutId { get; set; }


    public int SId { get; set; }


    public int RoomAssignmentId { get; set; }


    public DateTime CheckOutDate { get; set; }


    public int CheckedOutBy { get; set; }


    public string Reason { get; set; } = string.Empty;



    // Navigation

    public Student? Student { get; set; }


    public RoomAssignment? RoomAssignment { get; set; }


    public User? CheckedOutByUser { get; set; }
}