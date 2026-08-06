namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

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
=======
public class InspectionFinding
{
    public int Id { get; set; }
    public int InspectionId { get; set; }
    public string FindingDescription { get; set; } = string.Empty;
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
