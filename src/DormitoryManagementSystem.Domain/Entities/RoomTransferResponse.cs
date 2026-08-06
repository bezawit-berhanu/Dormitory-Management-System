namespace DormitoryManagementSystem.Domain.Entities;
public class RoomTransferResponse
{
    public int Id { get; set; }
    public int RoomTransferRequestId { get; set; }
    public bool IsApproved { get; set; }
    public DateTime ResponseDate { get; set; }
};