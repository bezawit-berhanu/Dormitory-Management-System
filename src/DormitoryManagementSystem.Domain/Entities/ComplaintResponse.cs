
using System.ComponentModel.DataAnnotations;

namespace DormitoryManagementSystem.Domain.Entities;

public class ComplaintResponse
{
    [Key]
    public int ResponseId { get; set; }

    // Foreign Key: Complaint
    public int ComplaintId { get; set; }
    public Complaint? Complaint { get; set; }

    // Foreign Key: User who responded
    public int RespondedByUserId { get; set; }
    public User? RespondedByUser { get; set; }

    public string Response { get; set; } = string.Empty;

    public DateTime ResponseDate { get; set; }
}

