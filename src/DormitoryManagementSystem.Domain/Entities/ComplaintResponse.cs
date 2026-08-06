namespace DormitoryManagementSystem.Domain.Entities;
public class ComplaintResponse
{
    public int Id { get; set; }
    public int ComplaintId { get; set; }
    public string ResponseText { get; set; } = string.Empty;
    public DateTime DateResponded { get; set; }
};
