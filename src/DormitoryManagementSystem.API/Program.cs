using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.Services;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using DormitoryManagementSystem.Infrastructure.Services;
using DormitoryManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Identity;
using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

Console.WriteLine(
    "DATABASE CONNECTION: " +
    builder.Configuration.GetConnectionString("DefaultConnection"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCors(options =>
{
    options.AddPolicy("FrontendDevelopment", policy =>
        policy.WithOrigins(
                "http://localhost:8080",
                "http://127.0.0.1:8080",
                "http://localhost:8081",
                "http://127.0.0.1:8081",
                "http://localhost:5173",
                "http://127.0.0.1:5173")
            .AllowAnyHeader()
            .AllowAnyMethod());

});




// Development
builder.Services.AddDbContext<ApplicationDbContext>(options =>

    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(StudentService).Assembly);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<IPasswordResetService, PasswordResetService>();
builder.Services.AddScoped<IEmailSender, SmtpEmailSender>();
builder.Services.AddScoped<IStaffAuthenticationService, StaffAuthenticationService>();
builder.Services.AddScoped<IStaffRegistrarService, StaffRegistrarService>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IUserManagementService, UserManagementService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();

builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();
builder.Services.AddScoped<IStaffRepository, StaffRepository>();

builder.Services.AddScoped<IRoomAssignmentService, RoomAssignmentService>();
builder.Services.AddScoped<IRoomAssignmentRepository, RoomAssignmentRepository>();

builder.Services.AddScoped<ICheckInService, CheckInService>();
builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();

builder.Services.AddScoped<ICheckOutService, CheckOutService>();
builder.Services.AddScoped<ICheckOutRepository, CheckOutRepository>();

builder.Services.AddScoped<IQRCodeService, QRCodeService>();
builder.Services.AddScoped<IQRCodeRepository, QRCodeRepository>();

builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();

builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<INotificationRepository, NotificationRepository>();

builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();

builder.Services.AddScoped<IInspectionService, InspectionService>();
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();

builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();

builder.Services.AddScoped<IViolationService, ViolationService>();
builder.Services.AddScoped<IViolationRepository, ViolationRepository>();

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();

var jwtKey = builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "JWT key is not configured.");
}

builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)),

                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();

builder.Services.AddHttpClient<IRegistrarService, RegistrarService>(
    client =>
    {
        client.BaseAddress =
            new Uri("http://localhost:5100/");
    });

builder.Services.AddHttpClient<IStaffRegistrarService, StaffRegistrarService>(
    client =>
    {
        client.BaseAddress =
            new Uri("http://localhost:5100/");
    });

var app = builder.Build();

app.UseMiddleware<ExceptionHandlingMiddleware>();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider
        .GetRequiredService<ApplicationDbContext>();

    await DbSeeder.SeedAsync(context);
}

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("FrontendDevelopment");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();