using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace DormitoryManagementSystem.Infrastructure.Data;

public class ApplicationDbContextFactory 
    : IDesignTimeDbContextFactory<ApplicationDbContext>
{
    public ApplicationDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        var connectionString =
            "server=mysql-d8067c-bezawit070-1234.c.aivencloud.com;port=15187;database=defaultdb;user=avnadmin;password=AVNS_u0MYYVozgKTEwhrbgAr";

        optionsBuilder.UseMySql(
            connectionString,
            ServerVersion.AutoDetect(connectionString)
        );

        return new ApplicationDbContext(optionsBuilder.Options);
    }
}