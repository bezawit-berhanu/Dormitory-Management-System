
using Microsoft.EntityFrameworkCore;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions options)
        : base(options)
    {
    }

  

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
        modelBuilder.Entity<ComplaintResponse>(entity =>
        {
            entity.HasKey(e => e.ResponseId);

            entity.HasOne(e => e.Complaint)
                .WithMany(c => c.Responses)
                .HasForeignKey(e => e.ComplaintId)
                .OnDelete(DeleteBehavior.Cascade);

            entity.HasOne(e => e.RespondedByUser)
                .WithMany()
                .HasForeignKey(e => e.RespondedByUserId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<InspectionFinding>(entity =>
        {
            entity.HasKey(e => e.FindingId);

            entity.HasOne(e => e.Inspection)
                .WithMany(i => i.Findings)
                .HasForeignKey(e => e.InspectionId)
                .OnDelete(DeleteBehavior.Cascade);
        });
        modelBuilder.Entity<MaintenanceActivity>()
    .HasKey(x => x.ActivityId);
        modelBuilder.Entity<MaintenanceActivity>()
        .HasOne<MaintenanceRequest>()
        .WithMany()
        .HasForeignKey(x => x.MaintenanceRequestId);

        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MaintenanceAssignment>()
    .HasKey(x => x.AssignmentId);
        modelBuilder.Entity<RoomTransferRequest>()
        .HasKey(x => x.TransferRequestId);
        modelBuilder.Entity<Bed>()
        .HasKey(x => x.BedId);
        modelBuilder.Entity<AuditLog>()
        .HasKey(x => x.AuditLogId);
        modelBuilder.Entity<CheckIn>()
        .HasKey(x => x.CheckInId);
        modelBuilder.Entity<CheckOut>()
        .HasKey(x => x.CheckOutId);
        modelBuilder.Entity<QRCode>()
        .HasKey(x => x.QRCodeId);
        modelBuilder.Entity<Violation>()
          .HasKey(x => x.ViolationId);
        modelBuilder.Entity<Complaint>()
  .HasKey(x => x.ComplaintId);
        modelBuilder.Entity<Announcement>()
        .HasKey(x => x.AnnouncementId);
        modelBuilder.Entity<Announcement>()
        .HasOne(a => a.CreatedByUser)
        .WithMany()
        .HasForeignKey(a => a.CreatedBy)
        .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Notification>()
            .HasKey(x => x.NotificationId);

        modelBuilder.Entity<RoomTransferResponse>()
            .HasKey(x => x.ResponseId);

        modelBuilder.Entity<Block>()
            .HasKey(x => x.BlockId);

        modelBuilder.Entity<Complaint>()
            .HasKey(x => x.ComplaintId);
        modelBuilder.Entity<Complaint>()
.HasOne(x => x.Student)
.WithMany()
.HasForeignKey(x => x.SId)
.OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Inspection>()
    .HasOne(i => i.Room)
    .WithMany()
    .HasForeignKey(i => i.RoomId)
    .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<Inspection>()
            .HasOne(i => i.InspectedByUser)
            .WithMany()
            // .HasForeignKey(i => i.InspectedBy)
            .HasPrincipalKey(u => u.UserId)
            .OnDelete(DeleteBehavior.Restrict);
        modelBuilder.Entity<InspectionFinding>()
            .HasKey(x => x.FindingId);

        modelBuilder.Entity<Floor>()
            .HasKey(x => x.FloorId);

        modelBuilder.Entity<Room>()
            .HasKey(x => x.RoomId);

        modelBuilder.Entity<Role>()
            .HasKey(x => x.RoleId);

        modelBuilder.Entity<User>()
            .HasKey(x => x.UserId);

        modelBuilder.Entity<Student>()
            .HasKey(x => x.StudentId);

        modelBuilder.Entity<Dormitory>()
            .HasKey(x => x.DormitoryId);

        modelBuilder.Entity<Department>()
            .HasKey(x => x.DepartmentId);

        modelBuilder.Entity<SecurityIncident>()
            .HasKey(x => x.IncidentId);


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

        // Configure EmergencyContact as an owned/value object
        modelBuilder.Entity<Student>()
            .OwnsOne(s => s.EmergencyContactNumber);
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
            .HasForeignKey(rtr => rtr.SId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<RoomTransferRequest>()
            .HasOne(rtr => rtr.CurrentRoom)
            .WithMany()
            .HasForeignKey(rtr => rtr.CurrentRoomId)
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
    }
}
}

