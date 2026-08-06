namespace DormitoryManagementSystem.Domain.Entities;
public class Complaint
{
    public int Id { get; set; }
    public int StudentId { get; set; }
    public string Description { get; set; } = string.Empty;
    public DateTime DateFiled { get; set; }
};
