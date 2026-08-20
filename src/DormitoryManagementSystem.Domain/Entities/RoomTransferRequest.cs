namespace DormitoryManagementSystem.Domain.Entities;

public class RoomTransferRequest
{
    public int TransferRequestId { get; set; }

    // Foreign Key: Student
    public int SId { get; set; }

    public Student? Student { get; set; }

    // Current Room
    public int CurrentRoomId { get; set; }

    public Room? CurrentRoom { get; set; }

    // Requested Room
    public int RequestedRoomId { get; set; }

    public Room? RequestedRoom { get; set; }

    public string Reason { get; set; } = string.Empty;

    public DateTime RequestDate { get; set; }

    public string Status { get; set; } = string.Empty;

    // User who approves the request
    public int ApprovedBy { get; set; }

    public User? ApprovedByUser { get; set; }
}