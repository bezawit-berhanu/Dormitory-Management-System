using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Infrastructure.Data;
using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.Services;
using DormitoryManagementSystem.Infrastructure.Repositories;
var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(
    "DATABASE CONNECTION: " +
    builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddScoped<IAnnouncementService, AnnouncementService>();
builder.Services.AddScoped<IAnnouncementRepository, AnnouncementRepository>();
builder.Services.AddScoped<INotificationService, NotificationService>();
builder.Services.AddScoped<DormitoryManagementSystem.Application.Interfaces.INotificationRepository, NotificationRepository>();
builder.Services.AddScoped<ComplaintService>();
builder.Services.AddScoped<IComplaintRepository, ComplaintRepository>();
builder.Services.AddScoped<IInspectionService, InspectionService>();
builder.Services.AddScoped<IInspectionRepository, InspectionRepository>();
// Services
builder.Services.AddScoped<IComplaintService, ComplaintService>();
builder.Services.AddScoped<ITransferService, TransferService>();
builder.Services.AddScoped<ITransferRepository, TransferRepository>();
builder.Services.AddScoped<IViolationService, ViolationService>();
builder.Services.AddScoped<IViolationRepository, ViolationRepository>();
builder.Services.AddOpenApi();

builder.Services.AddControllers();

builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
        builder.Configuration.GetConnectionString("DefaultConnection")
    ));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowReact",
        policy =>
        {
            policy.WithOrigins("http://localhost:5173")
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var app = builder.Build();


// Development
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowReact");
app.UseAuthorization();
app.MapControllers();

app.Run();