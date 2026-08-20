using System.Net.Mail;
using System.Text.RegularExpressions;

namespace DormitoryManagementSystem.Application.Validators;

public static class AuthenticationInputValidator
{
    private static readonly Regex PhoneNumberPattern = new(@"^(?:\+?[1-9]\d{7,14}|0\d{8,14})$", RegexOptions.Compiled);

    public static void ValidateRegistration(string email, string phoneNumber, string password, string confirmPassword)
    {
        if (!IsValidEmail(email))
            throw new ArgumentException("Enter a valid email address.");

        if (!PhoneNumberPattern.IsMatch(NormalizePhoneNumber(phoneNumber)))
            throw new ArgumentException("Enter a valid phone number, including the country code when applicable.");

        if (password != confirmPassword)
            throw new ArgumentException("Passwords do not match.");

        if (password.Length < 8 || !password.Any(char.IsUpper) || !password.Any(char.IsLower) ||
            !password.Any(char.IsDigit) || !password.Any(character => !char.IsLetterOrDigit(character)))
            throw new ArgumentException("Password must be at least 8 characters and include uppercase, lowercase, a number, and a symbol.");
    }

    public static string NormalizePhoneNumber(string phoneNumber) => phoneNumber.Replace(" ", string.Empty)
        .Replace("-", string.Empty).Replace("(", string.Empty).Replace(")", string.Empty);

    private static bool IsValidEmail(string email)
    {
        if (string.IsNullOrWhiteSpace(email)) return false;
        try
        {
            var address = new MailAddress(email.Trim());
            return string.Equals(address.Address, email.Trim(), StringComparison.OrdinalIgnoreCase);
        }
        catch (FormatException) { return false; }
    }
}
