using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Infrastructure.Data;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.Services;
using DormitoryManagementSystem.Infrastructure.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Services
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));
builder.Services.AddScoped<IMaintenanceService, MaintenanceService>();
builder.Services.AddScoped<IMaintenanceRepository, MaintenanceRepository>();
builder.Services.AddScoped<IDormitoryStructureRepository, DormitoryStructureRepository>();
builder.Services.AddScoped<IDormitoryStructureService, DormitoryStructureService>();
builder.Services.AddScoped<IMaintenanceAssignmentService, MaintenanceAssignmentService>();
builder.Services.AddScoped<IMaintenanceAssignmentRepository, MaintenanceAssignmentRepository>();
builder.Services.AddScoped<IMaintenanceActivityService, MaintenanceActivityService>();
builder.Services.AddScoped<IMaintenanceActivityRepository, MaintenanceActivityRepository>();
builder.Services.AddScoped<IReportService, ReportService>();
builder.Services.AddScoped<IDashboardService, DashboardService>();
builder.Services.AddScoped<IAuditService, AuditService>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();
builder.Services.AddScoped<IDashboardRepository, DashboardRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();