namespace DormitoryManagementSystem.Domain.Entities;
public class Violation
{
    public int Id { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateOccurred { get; set; }
};