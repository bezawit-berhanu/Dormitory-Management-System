using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Application.DTOs;

public class InspectionDto
{
    public int InspectionId { get; set; }

    [Required]
    public int RoomId { get; set; }

    [Required]
    public int InspectedByUserId { get; set; }
    public DateTime InspectionDate { get; set; }

    [Required]
    [StringLength(500)]
    public string Remarks { get; set; } = string.Empty;
    public string Status { get; set; } = "Completed";
}