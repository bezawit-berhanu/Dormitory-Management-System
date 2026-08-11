using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Configurations;

public class CheckInConfiguration : IEntityTypeConfiguration<CheckIn>
{
    public void Configure(EntityTypeBuilder<CheckIn> builder)
    {
        builder.HasKey(c => c.CheckInId);


        builder.HasOne(c => c.Student)
            .WithMany(s => s.CheckIns)
            .HasForeignKey(c => c.SId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.RoomAssignment)
            .WithMany()
            .HasForeignKey(c => c.RoomAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.CheckedInByUser)
     .WithMany()
     .HasForeignKey(c => c.CheckedInByUserId)
     .OnDelete(DeleteBehavior.Restrict);
    }
}