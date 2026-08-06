namespace DormitoryManagementSystem.Domain.Entities;

public class InspectionFinding
{
    public int FindingId { get; set; }


    // Foreign Key: Inspection
    public int InspectionId { get; set; }
    public Inspection? Inspection { get; set; }


    public string Finding { get; set; } = string.Empty;

    public string Severity { get; set; } = string.Empty;

    public string Recommendation { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;
}