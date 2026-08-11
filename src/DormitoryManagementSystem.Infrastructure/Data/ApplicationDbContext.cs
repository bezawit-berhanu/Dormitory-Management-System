
using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

    // ===============================
    // DbSets
    // ===============================

    public DbSet<Announcement> Announcements => Set<Announcement>();

    public DbSet<User> Users => Set<User>();
    public DbSet<Role> Roles => Set<Role>();

    public DbSet<Student> Students => Set<Student>();
    public DbSet<Department> Departments => Set<Department>();

    public DbSet<Dormitory> Dormitories => Set<Dormitory>();
    public DbSet<Block> Blocks => Set<Block>();
    public DbSet<Floor> Floors => Set<Floor>();
    public DbSet<Room> Rooms => Set<Room>();
    public DbSet<Bed> Beds => Set<Bed>();

    public DbSet<RoomAssignment> RoomAssignments => Set<RoomAssignment>();

    public DbSet<CheckIn> CheckIns => Set<CheckIn>();
    public DbSet<CheckOut> CheckOuts => Set<CheckOut>();

    public DbSet<QRCode> QRCodes => Set<QRCode>();

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public DbSet<Complaint> Complaints => Set<Complaint>();
    public DbSet<ComplaintResponse> ComplaintResponses => Set<ComplaintResponse>();

    public DbSet<Inspection> Inspections => Set<Inspection>();
    public DbSet<InspectionFinding> InspectionFindings => Set<InspectionFinding>();

    public DbSet<MaintenanceRequest> MaintenanceRequests => Set<MaintenanceRequest>();
    public DbSet<MaintenanceActivity> MaintenanceActivities => Set<MaintenanceActivity>();
    public DbSet<MaintenanceAssignment> MaintenanceAssignments => Set<MaintenanceAssignment>();

    public DbSet<Notification> Notifications => Set<Notification>();

    public DbSet<RoomTransferRequest> RoomTransferRequests => Set<RoomTransferRequest>();
    public DbSet<RoomTransferResponse> RoomTransferResponses => Set<RoomTransferResponse>();

    public DbSet<SecurityIncident> SecurityIncidents => Set<SecurityIncident>();

    public DbSet<Violation> Violations => Set<Violation>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(
            typeof(ApplicationDbContext).Assembly
        );

        // ===============================
        // Role
        // ===============================

        modelBuilder.Entity<Role>()
            .HasKey(r => r.RoleId);

        // ===============================
        // User
        // ===============================

        modelBuilder.Entity<User>()
            .HasKey(u => u.UserId);

        modelBuilder.Entity<User>()
            .HasOne(u => u.Role)
            .WithMany(r => r.Users)
            .HasForeignKey(u => u.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // Student
        // ===============================

        modelBuilder.Entity<Student>()
            .HasKey(s => s.SId);
        modelBuilder.Entity<Student>()
    .OwnsOne(s => s.EmergencyContact);

        modelBuilder.Entity<Student>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Student>()
                .HasOne<Department>()
                .WithMany()
                .HasForeignKey(s => s.DepartmentId)
                .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // Room Assignment
        // ===============================

        modelBuilder.Entity<RoomAssignment>()
            .HasKey(ra => ra.RoomAssignmentId);

        modelBuilder.Entity<RoomAssignment>()
            .HasOne(ra => ra.Student)
            .WithMany(s => s.RoomAssignments)
            .HasForeignKey(ra => ra.SId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomAssignment>()
            .HasOne(ra => ra.Room)
            .WithMany(r => r.RoomAssignments)
            .HasForeignKey(ra => ra.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomAssignment>()
            .HasOne(ra => ra.Bed)
            .WithMany(b => b.RoomAssignments)
            .HasForeignKey(ra => ra.BedId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomAssignment>()
            .HasOne(ra => ra.AssignedByUser)
            .WithMany()
            .HasForeignKey(ra => ra.AssignedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // Check In
        // ===============================

        modelBuilder.Entity<CheckIn>()
            .HasKey(c => c.CheckInId);

        modelBuilder.Entity<CheckIn>()
            .HasOne(c => c.Student)
            .WithMany(s => s.CheckIns)
            .HasForeignKey(c => c.SId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CheckIn>()
            .HasOne(c => c.RoomAssignment)
            .WithMany()
            .HasForeignKey(c => c.RoomAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CheckIn>()
            .HasOne(c => c.CheckedInByUser)
            .WithMany()
            .HasForeignKey(c => c.CheckedInByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // Check Out
        // ===============================

        modelBuilder.Entity<CheckOut>()
            .HasKey(c => c.CheckOutId);

        modelBuilder.Entity<CheckOut>()
            .HasOne(c => c.Student)
            .WithMany(s => s.CheckOuts)
            .HasForeignKey(c => c.SId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CheckOut>()
            .HasOne(c => c.RoomAssignment)
            .WithMany()
            .HasForeignKey(c => c.RoomAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<CheckOut>()
            .HasOne(c => c.CheckedOutByUser)
            .WithMany()
            .HasForeignKey(c => c.CheckedOutBy)
            .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // QR Code
        // ===============================

        modelBuilder.Entity<QRCode>()
            .HasKey(q => q.QRCodeId);

        modelBuilder.Entity<QRCode>()
            .HasOne(q => q.Student)
            .WithMany(s => s.QRCode)
            .HasForeignKey(q => q.SId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // Room
        // ===============================

        modelBuilder.Entity<Room>()
            .HasKey(r => r.RoomId);

        modelBuilder.Entity<Room>()
            .HasOne(r => r.Floor)
            .WithMany()
            .HasForeignKey(r => r.FloorId)
            .OnDelete(DeleteBehavior.Restrict);

        // ===============================
        // Bed
        // ===============================

        modelBuilder.Entity<Bed>()
            .HasKey(b => b.BedId);

        modelBuilder.Entity<Bed>()
            .HasOne(b => b.Room)
            .WithMany(r => r.Beds)
            .HasForeignKey(b => b.RoomId)
            .OnDelete(DeleteBehavior.Restrict);
        // ===============================
        // Room Transfer Request
        // ===============================

        modelBuilder.Entity<RoomTransferRequest>()
            .HasKey(r => r.TransferRequestId);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(r => r.CurrentRoom)
            .WithMany()
            .HasForeignKey(r => r.CurrentRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(r => r.RequestedRoom)
            .WithMany()
            .HasForeignKey(r => r.RequestedRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(r => r.Student)
            .WithMany()
            .HasForeignKey(r => r.SId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(r => r.ApprovedByUser)
            .WithMany()
            .HasForeignKey(r => r.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);
        // ===============================
        // Maintenance Activity
        // ===============================

        modelBuilder.Entity<MaintenanceActivity>()
            .HasKey(a => a.ActivityId);

        modelBuilder.Entity<MaintenanceActivity>()
            .HasOne(a => a.MaintenanceRequest)
            .WithMany()
            .HasForeignKey(a => a.MaintenanceRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceActivity>()
            .HasOne(a => a.PerformedByUser)
            .WithMany()
            .HasForeignKey(a => a.PerformedBy)
            .OnDelete(DeleteBehavior.Restrict);
        // ===============================
        // Maintenance Assignment
        // ===============================

        modelBuilder.Entity<MaintenanceAssignment>()
            .HasKey(a => a.AssignmentId);

        modelBuilder.Entity<MaintenanceAssignment>()
            .HasOne(a => a.MaintenanceRequest)
            .WithMany()
            .HasForeignKey(a => a.MaintenanceRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceAssignment>()
            .HasOne(a => a.User)
            .WithMany()
            .HasForeignKey(a => a.UserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}

