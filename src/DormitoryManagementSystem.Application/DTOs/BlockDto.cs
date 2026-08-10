namespace DormitoryManagementSystem.Application.DTOs;

public class BlockDto
{
    public int BlockId { get; set; }

    public string BlockName { get; set; } = string.Empty;

    public int DormitoryId { get; set; }

    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}