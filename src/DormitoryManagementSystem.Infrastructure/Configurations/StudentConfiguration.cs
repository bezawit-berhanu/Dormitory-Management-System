using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using DormitoryManagementSystem.Domain.Entities;

namespace DormitoryManagementSystem.Infrastructure.Configurations;

public class StudentConfiguration : IEntityTypeConfiguration<Student>
{
    public void Configure(EntityTypeBuilder<Student> builder)
    {
        builder.HasKey(s => s.SId);
        builder.Property(s => s.StudentId).IsRequired().HasMaxLength(50);
        builder.Property(s=> s.Gender).IsRequired().HasMaxLength(20);
        builder.Property(s=> s.Status).IsRequired();
        builder.HasOne(s =>s.User).WithMany().HasForeignKey(s =>s.UserId).OnDelete(DeleteBehavior.Restrict);
        builder.HasOne<Department>()
    .WithMany(d => d.Students)
    .HasForeignKey(s => s.DepartmentId)
    .OnDelete(DeleteBehavior.Restrict);
    }
}