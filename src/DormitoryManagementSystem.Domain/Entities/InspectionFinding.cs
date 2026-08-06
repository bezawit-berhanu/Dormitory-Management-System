namespace DormitoryManagementSystem.Domain.Entities;
public class InspectionFinding
{
    public int Id { get; set; }
    public int InspectionId { get; set; }
    public string FindingDescription { get; set; } = string.Empty;
};
