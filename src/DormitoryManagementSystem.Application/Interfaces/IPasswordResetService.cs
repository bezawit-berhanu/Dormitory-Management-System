namespace DormitoryManagementSystem.Application.Interfaces;

public interface IPasswordResetService
{
    Task RequestResetAsync(
        string email,
        CancellationToken cancellationToken = default);

    Task ResetPasswordAsync(
        string token,
        string newPassword,
        string confirmPassword,
        CancellationToken cancellationToken = default);
}
