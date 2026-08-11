using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Configurations;

public class CheckOutConfiguration : IEntityTypeConfiguration<CheckOut>
{
    public void Configure(EntityTypeBuilder<CheckOut> builder)
    {
        builder.HasKey(c => c.CheckOutId);


        builder.HasOne(c => c.Student)
            .WithMany(s => s.CheckOuts)
            .HasForeignKey(c => c.SId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.RoomAssignment)
            .WithMany()
            .HasForeignKey(c => c.RoomAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(c => c.CheckedOutByUser)
            .WithMany()
            .HasForeignKey(c => c.CheckedOutBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}