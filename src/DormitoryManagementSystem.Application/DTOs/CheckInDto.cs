namespace DormitoryManagementSystem.Application.DTOs;
public class CheckInDto
{
    public int CheckInId { get; set; }
    public string FullName { get; set; } = string.Empty;
    public string SID { get; set; } = string.Empty;

    public string Department { get; set; } = string.Empty;
}