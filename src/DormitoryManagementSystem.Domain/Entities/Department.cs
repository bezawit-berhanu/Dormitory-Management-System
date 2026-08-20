namespace DormitoryManagementSystem.Domain.Entities;

public class Department
{
    public int DepartmentId { get; set; }   // DMS database ID

    public int RegistrarDepartmentId { get; set; } // Registrar's ID

    public string DepartmentName { get; set; } = string.Empty;

    public ICollection<Student> Students { get; set; }
        = new List<Student>();
}