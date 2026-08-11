using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Application.DTOs;

public class ComplaintDto
{
    public int ComplaintId { get; set; }

    [Required]
    public int SId { get; set; }

    [Required]
    [StringLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required]
    [StringLength(1000)]
    public string Description { get; set; } = string.Empty;

    public string Status { get; set; } = "Pending";

    public DateTime ComplaintDate { get; set; } = DateTime.UtcNow;
    public string Priority { get; set; } = "Medium";
}