namespace DormitoryManagementSystem.Domain.Entities;

public class Block
{
    public int BlockId { get; set; }

    public string BlockName { get; set; } = string.Empty;

    public int DormitoryId { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public Dormitory Dormitory { get; set; } = null!;
}