namespace DormitoryManagementSystem.Domain.Entities;
public class Block
{
    public int BlockId { get; set; }
    public string BlockName { get; set; } = string.Empty;
public int DormitoryId { get; set; }
public string Description { get; set; } = string.Empty;
public Dormitory Dormitory { get; set; } = null!;
public ICollection<Floor> Floors { get; set; }
        = new List<Floor>();

};

