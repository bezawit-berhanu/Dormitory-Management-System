using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Application.DTOs;

public class ViolationDto
{
    public int ViolationId { get; set; }

    [Required]
    public int SId { get; set; }

    [Required]
    public string ViolationType { get; set; } = string.Empty;

    [StringLength(500)]
    public string Description { get; set; } = string.Empty;

    public DateTime ViolationDate { get; set; } = DateTime.UtcNow;
}