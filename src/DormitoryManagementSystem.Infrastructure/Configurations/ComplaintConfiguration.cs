using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Configurations;

public class ComplaintResponseConfiguration : IEntityTypeConfiguration<ComplaintResponse>
{
    public void Configure(EntityTypeBuilder<ComplaintResponse> builder)
    {
        // Primary Key
        builder.HasKey(cr => cr.ResponseId);

        // Complaint relationship
        builder.HasOne(cr => cr.Complaint)
            .WithMany()
            .HasForeignKey(cr => cr.ComplaintId)
            .OnDelete(DeleteBehavior.Restrict);

        // User relationship
        builder.HasOne(cr => cr.RespondedByUser)
            .WithMany()
            .HasForeignKey(cr => cr.RespondedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Response
        builder.Property(cr => cr.Response)
            .IsRequired();

        // Response date
        builder.Property(cr => cr.ResponseDate)
            .IsRequired();
    }
}