using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
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
    public DbSet<Staff> Staff => Set<Staff>();
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

        // Apply configurations from Infrastructure/Configurations
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
            .HasKey(s => s.StudentId);

        // Student -> User
        // No Student.User navigation exists,
        // therefore use WithMany().
        modelBuilder.Entity<Student>()
            .HasOne<User>()
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Student -> Department
        // No Student.Department navigation exists,
        // therefore use WithMany().
        modelBuilder.Entity<Student>()
            .HasOne<Department>()
            .WithMany()
            .HasForeignKey(s => s.DepartmentId)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Staff
        // ===============================

        modelBuilder.Entity<Staff>()
            .HasKey(s => s.StaffId);

        modelBuilder.Entity<Staff>()
            .HasOne(s => s.User)
            .WithMany()
            .HasForeignKey(s => s.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Department
        // ===============================

        modelBuilder.Entity<Department>()
            .HasKey(d => d.DepartmentId);


        // ===============================
        // Dormitory
        // ===============================

        modelBuilder.Entity<Dormitory>()
            .HasKey(d => d.DormitoryId);


        // ===============================
        // Block
        // ===============================

        modelBuilder.Entity<Block>()
            .HasKey(b => b.BlockId);


        // ===============================
        // Floor
        // ===============================

        modelBuilder.Entity<Floor>()
            .HasKey(f => f.FloorId);


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
        // Room Assignment
        // ===============================

        modelBuilder.Entity<RoomAssignment>()
            .HasKey(ra => ra.RoomAssignmentId);

        // Student navigation may exist on RoomAssignment,
        // but Student has no RoomAssignments collection.
        modelBuilder.Entity<RoomAssignment>()
            .HasOne(ra => ra.Student)
            .WithMany()
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
            .WithMany()
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
            .WithMany()
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

        // Student has no QRCodes collection.
        modelBuilder.Entity<QRCode>()
            .HasOne(q => q.Student)
            .WithMany()
            .HasForeignKey(q => q.SId)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Complaint
        // ===============================

        modelBuilder.Entity<Complaint>()
            .HasKey(c => c.ComplaintId);

        modelBuilder.Entity<Complaint>()
            .HasOne(c => c.Student)
            .WithMany()
            .HasForeignKey(c => c.SId)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Complaint Response
        // ===============================

        modelBuilder.Entity<ComplaintResponse>()
            .HasKey(cr => cr.ResponseId);

        modelBuilder.Entity<ComplaintResponse>()
            .HasOne(cr => cr.Complaint)
            .WithMany(c => c.Responses)
            .HasForeignKey(cr => cr.ComplaintId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<ComplaintResponse>()
            .HasOne(cr => cr.RespondedByUser)
            .WithMany()
            .HasForeignKey(cr => cr.RespondedByUserId)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Inspection
        // ===============================

        modelBuilder.Entity<Inspection>()
            .HasKey(i => i.InspectionId);

        modelBuilder.Entity<Inspection>()
            .HasOne(i => i.Room)
            .WithMany()
            .HasForeignKey(i => i.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        // The InspectedBy relationship is intentionally not
        // configured here because the current Inspection entity
        // does not expose an InspectedBy property.


        // ===============================
        // Inspection Finding
        // ===============================

        modelBuilder.Entity<InspectionFinding>()
            .HasKey(f => f.FindingId);

        modelBuilder.Entity<InspectionFinding>()
            .HasOne(f => f.Inspection)
            .WithMany(i => i.Findings)
            .HasForeignKey(f => f.InspectionId)
            .OnDelete(DeleteBehavior.Cascade);


        // ===============================
        // Maintenance Request
        // ===============================

        modelBuilder.Entity<MaintenanceRequest>()
            .HasKey(m => m.MaintenanceRequestId);


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
            .HasKey(ma => ma.AssignmentId);

        modelBuilder.Entity<MaintenanceAssignment>()
            .HasOne(ma => ma.MaintenanceRequest)
            .WithMany()
            .HasForeignKey(ma => ma.MaintenanceRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<MaintenanceAssignment>()
            .HasOne(ma => ma.User)
            .WithMany()
            .HasForeignKey(ma => ma.UserId)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Room Transfer Request
        // ===============================

        modelBuilder.Entity<RoomTransferRequest>()
            .HasKey(rtr => rtr.TransferRequestId);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(rtr => rtr.Student)
            .WithMany()
.HasForeignKey(rtr => rtr.CurrentRoomId).OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(x => x.Student)
            .WithMany()
            .HasForeignKey(x => x.SId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(rtr => rtr.RequestedRoom)
            .WithMany()
            .HasForeignKey(rtr => rtr.RequestedRoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(rtr => rtr.ApprovedByUser)
            .WithMany()
            .HasForeignKey(rtr => rtr.ApprovedBy)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Room Transfer Response
        // ===============================

        modelBuilder.Entity<RoomTransferResponse>()
            .HasKey(rtr => rtr.ResponseId);

        modelBuilder.Entity<RoomTransferResponse>()
            .HasOne(rtr => rtr.TransferRequest)
            .WithMany()
            .HasForeignKey(rtr => rtr.TransferRequestId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferResponse>()
            .HasOne(rtr => rtr.RespondedByUser)
            .WithMany()
            .HasForeignKey(rtr => rtr.RespondedBy)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Announcement
        // ===============================

        modelBuilder.Entity<Announcement>()
            .HasKey(a => a.AnnouncementId);

        modelBuilder.Entity<Announcement>()
            .HasOne(a => a.CreatedByUser)
            .WithMany()
            .HasForeignKey(a => a.CreatedBy)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Notification
        // ===============================

        modelBuilder.Entity<Notification>()
            .HasKey(n => n.NotificationId);


        // ===============================
        // Audit Log
        // ===============================

        modelBuilder.Entity<AuditLog>()
            .HasKey(a => a.AuditLogId);


        // ===============================
        // Security Incident
        // ===============================

        modelBuilder.Entity<SecurityIncident>()
            .HasKey(si => si.IncidentId);

        modelBuilder.Entity<SecurityIncident>()
            .HasOne(si => si.Room)
            .WithMany()
            .HasForeignKey(si => si.RoomId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<SecurityIncident>()
            .HasOne(si => si.ReportedByUser)
            .WithMany()
            .HasForeignKey(si => si.ReportedBy)
            .OnDelete(DeleteBehavior.Restrict);


        // ===============================
        // Violation
        // ===============================

        modelBuilder.Entity<Violation>()
            .HasKey(v => v.ViolationId);
    }
}