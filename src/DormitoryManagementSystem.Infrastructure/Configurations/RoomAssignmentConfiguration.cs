using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Configurations;

public class RoomAssignmentConfiguration : IEntityTypeConfiguration<RoomAssignment>
{
    public void Configure(EntityTypeBuilder<RoomAssignment> builder)
    {
        builder.HasKey(ra => ra.RoomAssignmentId);
        builder.HasOne(ra => ra.Student).WithMany(s=> s.RoomAssignments).HasForeignKey(ra => ra.SId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne(ra => ra.Room).WithMany(room => room.RoomAssignments).HasForeignKey(ra => ra.RoomId).OnDelete(DeleteBehavior.Restrict);
    builder.HasOne(ra => ra.Bed).WithMany().HasForeignKey(ra => ra.BedId).OnDelete(DeleteBehavior.Restrict);
builder.HasOne(ra =>ra.AssignedByUser).WithMany().HasForeignKey(ra => ra.AssignedByUserId).OnDelete(DeleteBehavior.Restrict);
    }
}