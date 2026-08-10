namespace DormitoryManagementSystem.Domain.Entities;

public class Floor
{
    public int FloorId { get; set; }

    public string FloorNumber { get; set; } = string.Empty;

    public int BlockId { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;

    public Block Block { get; set; } = null!;
}