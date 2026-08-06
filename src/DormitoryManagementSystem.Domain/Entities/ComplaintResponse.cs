namespace DormitoryManagementSystem.Domain.Entities;
<<<<<<< HEAD

public class ComplaintResponse
{
    public int ResponseId { get; set; }

    // Foreign Key: Complaint
    public int ComplaintId { get; set; }
    public Complaint? Complaint { get; set; }


    // Foreign Key: User who responded
    public int RespondedByUserId { get; set; }
    public User? RespondedByUser { get; set; }


    public string Response { get; set; } = string.Empty;

    public DateTime ResponseDate { get; set; }
}
=======
public class ComplaintResponse
{
    public int Id { get; set; }
    public int ComplaintId { get; set; }
    public string ResponseText { get; set; } = string.Empty;
    public DateTime DateResponded { get; set; }
};
>>>>>>> ca6ddbeeb2d8d4bf0827dae4d9461809e589ba28
