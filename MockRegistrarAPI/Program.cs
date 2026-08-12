var builder = WebApplication.CreateBuilder(args);

// ==========================================
// SERVICES
// ==========================================

// Allows ASP.NET to discover our controllers.
builder.Services.AddControllers();

// Allows the Mock Registrar API to be called
// from our React frontend running on port 5173.
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend", policy =>
    {
        policy
            .WithOrigins("http://localhost:5173")
            .AllowAnyHeader()
            .AllowAnyMethod();
    });
});


var app = builder.Build();


// ==========================================
// HTTP PIPELINE
// ==========================================

// Allow React to communicate with this API.
app.UseCors("AllowFrontend");


//
app.MapControllers();


// Start the API.
app.Run();