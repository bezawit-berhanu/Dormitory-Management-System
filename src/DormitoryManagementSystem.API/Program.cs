using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.Services;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
using DormitoryManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddAutoMapper(typeof(StudentService).Assembly);

builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddScoped<IStudentService, StudentService>();

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

var app = builder.Build();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();