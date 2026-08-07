using DormitoryManagementSystem.Domain.ValueObjects;
using DormitoryManagementSystem.Domain.Enums;
namespace DormitoryManagementSystem.Domain.Entities;

public class Student
{
    public int SId { get; set; }


    public int UserId { get; set; }


    public int DepartmentId { get; set; }


    public string StudentId { get; set; } = string.Empty;


    public string Gender { get; set; } = string.Empty;


    public DateTime DateOfBirth { get; set; }


    public int YearOfStudy { get; set; }


    public UserStatus Status { get; set; }


    public EmergencyContact? EmergencyContact { get; set; }



    // Navigation

    public User? User { get; set; }


    public ICollection<RoomAssignment> RoomAssignments { get; set; }
        = new List<RoomAssignment>();


    public ICollection<CheckIn> CheckIns { get; set; }
        = new List<CheckIn>();


    public ICollection<CheckOut> CheckOuts { get; set; }
        = new List<CheckOut>();


    public ICollection<QRCode> QRCode { get; set; }
        = new List<QRCode>();
}