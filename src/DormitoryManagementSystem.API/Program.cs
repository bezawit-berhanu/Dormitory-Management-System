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
using Microsoft.Extensions.Identity.Core;
using DormitoryManagementSystem.Domain.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddAutoMapper(typeof(StudentService).Assembly);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<
    IStaffAuthenticationService,
    StaffAuthenticationService
>();

builder.Services.AddScoped<
    IStaffRegistrarService,
    StaffRegistrarService
>();

builder.Services.AddScoped<
    IStaffRepository,
    StaffRepository
>();

builder.Services.AddScoped<IStudentService, StudentService>();
builder.Services.AddScoped< IUserManagementService,UserManagementService>();
builder.Services.AddScoped<IDepartmentRepository, DepartmentRepository>();

builder.Services.AddScoped<IRoomAssignmentService, RoomAssignmentService>();

builder.Services.AddScoped<ICheckInService, CheckInService>();

builder.Services.AddScoped<ICheckOutService, CheckOutService>();

builder.Services.AddScoped<IQRCodeService, QRCodeService>();

builder.Services.AddScoped<ICheckInRepository, CheckInRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();

builder.Services.AddScoped<IRoomAssignmentRepository, RoomAssignmentRepository>();

builder.Services.AddScoped<IQRCodeRepository, QRCodeRepository>();

builder.Services.AddScoped<ICheckOutRepository, CheckOutRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();


builder.Services.AddScoped<IPasswordHasher<User>, PasswordHasher<User>>();
builder.Services.AddHttpClient<IRegistrarService, RegistrarService>(
    client =>
    {
        client.BaseAddress =
            new Uri("http://localhost:5100/");
    });

builder.Services.AddHttpClient<
    IStaffRegistrarService,
    StaffRegistrarService
>(
    client =>
    {
        client.BaseAddress =
            new Uri("http://localhost:5100/");
    });
   var jwtKey =
    builder.Configuration["Jwt:Key"];

if (string.IsNullOrWhiteSpace(jwtKey))
{
    throw new InvalidOperationException(
        "JWT key is not configured."
    );
}

builder.Services
    .AddAuthentication(
        JwtBearerDefaults.AuthenticationScheme
    )
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters =
            new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,

                IssuerSigningKey =
                    new SymmetricSecurityKey(
                        Encoding.UTF8.GetBytes(jwtKey)
                    ),

                ValidateIssuer = false,
                ValidateAudience = false,

                ValidateLifetime = true,

                ClockSkew = TimeSpan.Zero
            };
    });

builder.Services.AddAuthorization();

var app = builder.Build();
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
app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();