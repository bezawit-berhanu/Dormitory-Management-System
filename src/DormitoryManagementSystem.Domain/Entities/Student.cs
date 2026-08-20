using DormitoryManagementSystem.Domain.ValueObjects;
using DormitoryManagementSystem.Domain.Enums;

namespace DormitoryManagementSystem.Domain.Entities;

public class Student
{
    public string StudentId { get; set; } = string.Empty;
    public int SId { get; set; }


    public int UserId { get; set; }

    public string Name { get; set; } = string.Empty;

    public string StudentNumber { get; set; } = string.Empty;

    public int DepartmentId { get; set; }

    public string Gender { get; set; } = string.Empty;

    public DateTime DateOfBirth { get; set; }

    public string YearOfStudy { get; set; } = string.Empty;

    public string EmergencyContactNumber { get; set; } = string.Empty;

    public string Status { get; set; } = string.Empty;

    // Navigation properties
    public User? User { get; set; }

    public Department? Department { get; set; }

    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();

    public ICollection<CheckIn> CheckIns { get; set; }
        = new List<CheckIn>();

    public ICollection<CheckOut> CheckOuts { get; set; }
        = new List<CheckOut>();

    public ICollection<QRCode> QRCodes { get; set; }
        = new List<QRCode>();
}