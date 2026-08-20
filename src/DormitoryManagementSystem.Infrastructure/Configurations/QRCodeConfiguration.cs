using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Configurations;

public class QRCodeConfiguration : IEntityTypeConfiguration<QRCode>
{
    public void Configure(EntityTypeBuilder<QRCode> builder)
    {
        builder.HasKey(q => q.QRCodeId);


        builder.Property(q => q.QRCodeValue)
            .IsRequired()
            .HasMaxLength(200);


        builder.HasOne(q => q.Student)
            .WithMany(s => s.QRCodes)
            .HasForeignKey(q => q.SId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}