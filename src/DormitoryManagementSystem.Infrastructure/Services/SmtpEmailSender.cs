using System.Net;
using System.Net.Mail;
using DormitoryManagementSystem.Application.Interfaces;
using Microsoft.Extensions.Configuration;

namespace DormitoryManagementSystem.Infrastructure.Services;

public sealed class SmtpEmailSender : IEmailSender
{
    private readonly IConfiguration _configuration;

    public SmtpEmailSender(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task SendAsync(
        string recipient,
        string subject,
        string htmlBody,
        CancellationToken cancellationToken = default)
    {
        var host = _configuration["Email:Smtp:Host"];
        var from = _configuration["Email:Smtp:From"];
        if (string.IsNullOrWhiteSpace(host) || string.IsNullOrWhiteSpace(from))
            throw new InvalidOperationException("SMTP email settings are not configured.");

        var port = int.TryParse(_configuration["Email:Smtp:Port"], out var configuredPort)
            ? configuredPort
            : 587;
        var enableSsl = !bool.TryParse(_configuration["Email:Smtp:EnableSsl"], out var configuredSsl) || configuredSsl;

        using var message = new MailMessage(from, recipient)
        {
            Subject = subject,
            Body = htmlBody,
            IsBodyHtml = true,
        };
        using var client = new SmtpClient(host, port)
        {
            EnableSsl = enableSsl,
        };

        var username = _configuration["Email:Smtp:Username"];
        var password = _configuration["Email:Smtp:Password"];
        if (!string.IsNullOrWhiteSpace(username))
            client.Credentials = new NetworkCredential(username, password);

        await client.SendMailAsync(message, cancellationToken);
    }
}
