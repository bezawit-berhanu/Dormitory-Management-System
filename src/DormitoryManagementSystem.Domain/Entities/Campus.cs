namespace DormitoryManagementSystem.Domain.Entities;

public class Campus
{
 public int CampusId { get; set; }

 public string CampusName { get; set; } = string.Empty;

 public string Location { get; set; } = string.Empty;

    // Navigation
public ICollection<Dormitory> Dormitories { get; set; }
= new List<Dormitory>();

}