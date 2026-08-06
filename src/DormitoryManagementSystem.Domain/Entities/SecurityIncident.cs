namespace DormitoryManagementSystem.Domain.Entities;
public class SecurityIncident
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateReported { get; set; }
};