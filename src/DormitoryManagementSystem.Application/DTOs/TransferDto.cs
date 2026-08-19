using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Application.DTOs;

public class TransferDto
{
    public int RoomTransferRequestId { get; set; }

    [Required]
    public int SId { get; set; }

    [Required]
    public int CurrentRoomId { get; set; }

    [Required]
    public int RequestedRoomId { get; set; }

    [Required]
    [StringLength(500)]
    public string Reason { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime RequestDate { get; set; } = DateTime.UtcNow;
}