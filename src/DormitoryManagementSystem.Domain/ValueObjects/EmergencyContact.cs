namespace DormitoryManagementSystem.Domain.ValueObjects;
public class EmergencyContact
{
    public string FullName { get; set; } = string.Empty;
    public string PhoneNumber { get; set; } = string.Empty;
    public string Relationship { get; set; } = string.Empty;
}