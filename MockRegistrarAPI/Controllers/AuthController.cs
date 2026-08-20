using Microsoft.AspNetCore.Mvc;

namespace MockRegistrarAPI.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController : ControllerBase
{

    [HttpPost("login")]
    public IActionResult Login([FromBody] LoginRequest request)
    {
        // Temporary test credentials.
        if (request.Email != "admin@test.com" ||
            request.Password != "Admin123!")
        {
            return Unauthorized(new
            {
                message = "Invalid email or password."
            });
        }

        // Fake user returned after successful login.
        var user = new
        {
            id = 1,
            fullName = "Test Administrator",
            email = "admin@test.com",
            role = "Admin"
        };

        // Temporary fake token.
        
        // It is NOT a real JWT.
        // It only allows us to test the frontend
        // authentication flow.
        var token = "mocktest-token";

        return Ok(new
        {
            token = token,
            user = user
        });
    }
}


public class LoginRequest
{
    public string Email { get; set; } = string.Empty;

    public string Password { get; set; } = string.Empty;
}