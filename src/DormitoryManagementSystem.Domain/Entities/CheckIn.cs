namespace DormitoryManagementSystem.Domain.Entities;
public class CheckIn
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckInDate { get; set; }
};
