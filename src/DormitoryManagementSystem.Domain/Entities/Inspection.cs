namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

public class Inspection
{
    public int InspectionId { get; set; }

    // Foreign Key: Room
    public int RoomId { get; set; }
    public Room? Room { get; set; }


    // Foreign Key: User who inspected the room
    public int InspectedByUserId { get; set; }
    public User? InspectedByUser { get; set; }


    public DateTime InspectionDate { get; set; }

    public string Status { get; set; } = string.Empty;

    public string Remarks { get; set; } = string.Empty;


    // One Inspection can have many findings
    public ICollection<InspectionFinding> Findings { get; set; } = new List<InspectionFinding>();
}
=======
public class Inspection
{
    public int Id { get; set; }
    public int RoomId { get; set; }
    public DateTime InspectionDate { get; set; }
    public string InspectorName { get; set; } = string.Empty;
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
