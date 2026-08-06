namespace DormitoryManagementSystem.Domain.Entities;
public class CheckOut
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public int RoomId { get; set; }
    public DateTime CheckOutDate { get; set; }
};
