namespace DormitoryManagementSystem.Application.Interfaces;

public interface IJwtService
{
    string GenerateToken(
        int userId,
        string identifier,
        string role
    );
}