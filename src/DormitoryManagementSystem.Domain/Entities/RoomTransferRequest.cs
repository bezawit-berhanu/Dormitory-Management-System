namespace DormitoryManagementSystem.Domain.Entities;
public class RoomTransferRequest
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int FromRoomId { get; set; }
    public int ToRoomId { get; set; }
    public DateTime RequestDate { get; set; }
};