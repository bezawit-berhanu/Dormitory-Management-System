using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Announcement> Announcements => Set<Announcement>();
    public DbSet<User> Users => Set<User>();
    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();
    public DbSet<Bed> Beds => Set<Bed>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<CheckIn> CheckIns => Set<CheckIn>();
    public DbSet<CheckOut> CheckOuts => Set<CheckOut>();
    public DbSet<Complaint> Complaints => Set<Complaint>();
    public DbSet<ComplaintResponse> ComplaintResponses => Set<ComplaintResponse>();
    public DbSet<Dormitory> Dormitories => Set<Dormitory>();
    public DbSet<Floor> Floors => Set<Floor>();
    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<InspectionFinding> InspectionFindings => Set<InspectionFinding>();
    public DbSet<MaintenanceActivity> MaintenanceActivities => Set<MaintenanceActivity>();
    public DbSet<MaintenanceRequest> MaintenanceRequests => Set<MaintenanceRequest>();
    public DbSet<Notification> Notifications => Set<Notification>();
    public DbSet<QRCode> QRCodes => Set<QRCode>();
    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Student> Students => Set<Student>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<RoomAssignment> RoomAssignments => Set<RoomAssignment>();
    public DbSet<RoomTransferRequest> RoomTransferRequests => Set<RoomTransferRequest>();
    public DbSet<RoomTransferResponse> RoomTransferResponses => Set<RoomTransferResponse>();
    public DbSet<SecurityIncident> SecurityIncidents => Set<SecurityIncident>();
    public DbSet<Violation> Violations => Set<Violation>();
    public DbSet<Department> Departments => Set<Department>();
    public DbSet<MaintenanceAssignment> MaintenanceAssignments => Set<MaintenanceAssignment>();


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}