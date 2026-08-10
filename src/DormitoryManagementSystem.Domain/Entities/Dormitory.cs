namespace DormitoryManagementSystem.Domain.Entities;

public class Dormitory
{
    public int DormitoryId { get; set; }

    public string DormitoryName { get; set; } = string.Empty;

    public string Location { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}