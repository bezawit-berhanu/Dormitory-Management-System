using DormitoryManagementSystem.Application.Interfaces;
using DormitoryManagementSystem.Application.Services;
using DormitoryManagementSystem.Domain.Interfaces;
using DormitoryManagementSystem.Infrastructure.Data;
<<<<<<< HEAD
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
=======
using DormitoryManagementSystem.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
>>>>>>> origin

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
<<<<<<< HEAD
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
=======

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
>>>>>>> origin
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
<<<<<<< HEAD
app.UseCors("AllowReact");
app.UseAuthorization();
=======

app.UseAuthorization();

>>>>>>> origin
app.MapControllers();

app.Run();