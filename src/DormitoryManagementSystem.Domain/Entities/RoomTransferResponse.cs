using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Domain.Entities;

public class RoomTransferResponse
{
    [Key]
    public int ResponseId { get; set; }

    public int TransferRequestId { get; set; }

    public int RespondedBy { get; set; }

    public string ResponseMessage { get; set; } = string.Empty;

    public string Decision { get; set; } = string.Empty;

    public DateTime ResponseDate { get; set; }

    public RoomTransferRequest TransferRequest { get; set; } = null!;

    public User RespondedByUser { get; set; } = null!;
}