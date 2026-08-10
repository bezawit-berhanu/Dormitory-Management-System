namespace DormitoryManagementSystem.Domain.Entities;
public class Department
{
    public int DepartmentId { get; set; }

    public string DepartmentName { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; } = new List<Student>();
};