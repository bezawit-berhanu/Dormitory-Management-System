namespace DormitoryManagementSystem.Domain.Entities;

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
