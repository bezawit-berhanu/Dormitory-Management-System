namespace DormitoryManagementSystem.Domain.Entities;
public class Floor
{
    public int Id { get; set; }
    public string FloorNumber { get; set; } = string.Empty;
    public int BlockId { get; set; }
};
