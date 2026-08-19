using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

using DormitoryManagementSystem.Application.Interfaces;

using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;

namespace DormitoryManagementSystem.Infrastructure.Services;

public class JwtService : IJwtService
{
    private readonly IConfiguration _configuration;

    public JwtService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public string GenerateToken(
        int userId,
        string identifier,
        string role)
    {
        var key =
            _configuration["Jwt:Key"];

        if (string.IsNullOrWhiteSpace(key))
        {
            throw new InvalidOperationException(
                "JWT key is not configured."
            );
        }

        var securityKey =
            new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(key)
            );

        var credentials =
            new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256
            );

        var claims = new[]
        {
            new Claim(
                ClaimTypes.NameIdentifier,
                userId.ToString()
            ),

            new Claim(
                ClaimTypes.Name,
                identifier
            ),

            new Claim(
                ClaimTypes.Role,
                role
            )
        };

        var token =
            new JwtSecurityToken(
                claims: claims,
                expires: DateTime.UtcNow.AddHours(8),
                signingCredentials: credentials
            );

        return new JwtSecurityTokenHandler()
            .WriteToken(token);
    }
}