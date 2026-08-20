using System.Net;

namespace DormitoryManagementSystem.API.Middleware;

public sealed class ExceptionHandlingMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionHandlingMiddleware> _logger;

    public ExceptionHandlingMiddleware(
        RequestDelegate next,
        ILogger<ExceptionHandlingMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception exception)
        {
            _logger.LogError(exception, "Unhandled exception while processing {Method} {Path}",
                context.Request.Method, context.Request.Path);

            if (context.Response.HasStarted)
            {
                throw;
            }

            var (statusCode, message) = exception switch
            {
                ArgumentException => (HttpStatusCode.BadRequest, "Please check the information you entered and try again."),
                UnauthorizedAccessException => (HttpStatusCode.Unauthorized, "You need to sign in to perform this action."),
                KeyNotFoundException => (HttpStatusCode.NotFound, "The requested record could not be found."),
                _ => (HttpStatusCode.InternalServerError, "A server issue occurred. Please try again shortly.")
            };

            context.Response.Clear();
            context.Response.StatusCode = (int)statusCode;
            context.Response.ContentType = "application/json";
            await context.Response.WriteAsJsonAsync(new { message });
        }
    }
}
