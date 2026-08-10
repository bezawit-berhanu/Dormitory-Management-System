namespace DormitoryManagementSystem.Domain.Entities;

public class Complaint
{
    public int ComplaintId { get; set; }

    // Foreign Key: Student
    public int SId { get; set; }
    public Student? Student { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Description { get; set; } = string.Empty;

    public DateTime ComplaintDate { get; set; }

    public string Priority { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;


    // Navigation: One Complaint can have many responses
    public ICollection<ComplaintResponse> Responses { get; set; } = new List<ComplaintResponse>();
};
