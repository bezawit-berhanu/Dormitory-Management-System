using System.Net;
using System.Security.Cryptography;
using System.Text;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;

namespace DormitoryManagementSystem.Application.Services;

public sealed class PasswordResetService : IPasswordResetService
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher<User> _passwordHasher;
    private readonly IEmailSender _emailSender;
    private readonly byte[] _signingKey;
    private readonly string _frontendBaseUrl;

    public PasswordResetService(
        IUserRepository userRepository,
        IPasswordHasher<User> passwordHasher,
        IEmailSender emailSender,
        IConfiguration configuration)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _emailSender = emailSender;

        var jwtKey = configuration["Jwt:Key"];
        if (string.IsNullOrWhiteSpace(jwtKey))
            throw new InvalidOperationException("JWT key is not configured.");

        _signingKey = Encoding.UTF8.GetBytes(jwtKey);
        _frontendBaseUrl = (configuration["PasswordReset:FrontendBaseUrl"] ?? "http://localhost:8080").TrimEnd('/');
    }

    public async Task RequestResetAsync(
        string email,
        CancellationToken cancellationToken = default)
    {
        var normalizedEmail = email.Trim();
        if (string.IsNullOrWhiteSpace(normalizedEmail))
            return;

        var user = await _userRepository.GetByEmailAsync(normalizedEmail);
        if (user is null)
            return;

        var expiresAt = DateTimeOffset.UtcNow.AddMinutes(30);
        var payload = EncodePayload($"{user.Email}|{expiresAt.ToUnixTimeSeconds()}");
        var signature = Sign(payload, user.PasswordHash);
        var token = $"{payload}.{signature}";
        var resetUrl = $"{_frontendBaseUrl}/reset-password?token={Uri.EscapeDataString(token)}";
        var safeResetUrl = WebUtility.HtmlEncode(resetUrl);

        await _emailSender.SendAsync(
            user.Email,
            "Reset your Dwell password",
            $"<p>We received a request to reset your Dwell password.</p><p><a href=\"{safeResetUrl}\">Reset your password</a></p><p>This link expires in 30 minutes. If you did not request this, you can ignore this email.</p>",
            cancellationToken);
    }

    public async Task ResetPasswordAsync(
        string token,
        string newPassword,
        string confirmPassword,
        CancellationToken cancellationToken = default)
    {
        if (string.IsNullOrWhiteSpace(newPassword) || newPassword.Length < 8)
            throw new ArgumentException("Password must contain at least 8 characters.");

        if (!string.Equals(newPassword, confirmPassword, StringComparison.Ordinal))
            throw new ArgumentException("Passwords do not match.");

        var user = await GetUserFromTokenAsync(token);
        if (user is null)
            throw new ArgumentException("This reset link is invalid or expired.");

        user.PasswordHash = _passwordHasher.HashPassword(user, newPassword);
        await _userRepository.UpdateAsync(user);
        await _userRepository.SaveChangesAsync();
    }

    private async Task<User?> GetUserFromTokenAsync(string token)
    {
        var parts = token.Split('.', StringSplitOptions.RemoveEmptyEntries);
        if (parts.Length != 2)
            return null;

        string decodedPayload;
        try
        {
            decodedPayload = DecodePayload(parts[0]);
        }
        catch (FormatException)
        {
            return null;
        }

        var payloadParts = decodedPayload.Split('|', 2);
        if (payloadParts.Length != 2 || !long.TryParse(payloadParts[1], out var expiresAt))
            return null;

        DateTimeOffset expiration;
        try
        {
            expiration = DateTimeOffset.FromUnixTimeSeconds(expiresAt);
        }
        catch (ArgumentOutOfRangeException)
        {
            return null;
        }

        if (DateTimeOffset.UtcNow >= expiration)
            return null;

        var user = await _userRepository.GetByEmailAsync(payloadParts[0]);
        if (user is null)
            return null;

        var expectedSignature = Sign(parts[0], user.PasswordHash);
        var actualSignature = DecodeSignature(parts[1]);
        var expectedSignatureBytes = DecodeSignature(expectedSignature);
        if (actualSignature is null || expectedSignatureBytes is null ||
            !CryptographicOperations.FixedTimeEquals(expectedSignatureBytes, actualSignature))
            return null;

        return user;
    }

    private string Sign(string payload, string passwordHash)
    {
        var value = Encoding.UTF8.GetBytes($"{payload}.{passwordHash}");
        return EncodeBytes(HMACSHA256.HashData(_signingKey, value));
    }

    private static string EncodePayload(string value) => EncodeBytes(Encoding.UTF8.GetBytes(value));

    private static string DecodePayload(string value) => Encoding.UTF8.GetString(DecodeBytes(value));

    private static string EncodeBytes(byte[] value) => Convert.ToBase64String(value)
        .TrimEnd('=')
        .Replace('+', '-')
        .Replace('/', '_');

    private static byte[] DecodeBytes(string value)
    {
        var padded = value.Replace('-', '+').Replace('_', '/');
        padded = padded.PadRight(padded.Length + (4 - padded.Length % 4) % 4, '=');
        return Convert.FromBase64String(padded);
    }

    private static byte[]? DecodeSignature(string value)
    {
        try
        {
            return DecodeBytes(value);
        }
        catch (FormatException)
        {
            return null;
        }
    }
}
