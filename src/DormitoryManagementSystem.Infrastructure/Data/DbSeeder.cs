using DormitoryManagementSystem.Domain.Entities;
using DormitoryManagementSystem.Domain.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;


namespace DormitoryManagementSystem.Infrastructure.Data;

public static class DbSeeder
{
    public static async Task SeedAsync(ApplicationDbContext context)
    {
        var passwordHasher = new PasswordHasher<User>();


// ROLES


var adminRole = await context.Roles
    .FirstOrDefaultAsync(r => r.RoleName == "Admin");

if (adminRole == null)
{
    adminRole = new Role
    {
        RoleName = "Admin"
    };

    context.Roles.Add(adminRole);
}

var staffRole = await context.Roles
    .FirstOrDefaultAsync(r => r.RoleName == "Staff");

if (staffRole == null)
{
    staffRole = new Role
    {
        RoleName = "Staff"
    };

    context.Roles.Add(staffRole);
}

var studentRole = await context.Roles
    .FirstOrDefaultAsync(r => r.RoleName == "Student");

if (studentRole == null)
{
    studentRole = new Role
    {
        RoleName = "Student"
    };

    context.Roles.Add(studentRole);
}

await context.SaveChangesAsync();



// TEST ADMIN / STAFF USERS

 adminRole = await context.Roles
    .FirstAsync(r => r.RoleName == "Admin");

staffRole = await context.Roles
    .FirstAsync(r => r.RoleName == "Staff");




if (!await context.Users.AnyAsync(u =>
    u.Email == "admin@dormitory.com"))
{
    var admin = new User
    {
        FullName = "System Administrator",
        Email = "admin@dormitory.com",
        PhoneNumber = "0911111111",
        RoleId = adminRole.RoleId,
        Status = UserStatus.Active,
        CreatedAt = DateTime.UtcNow
    };

    admin.PasswordHash =
        passwordHasher.HashPassword(
            admin,
            "Admin123"
        );

    context.Users.Add(admin);
}


if (!await context.Users.AnyAsync(u =>
    u.Email == "staff@dormitory.com"))
{
    var staff = new User
    {
        FullName = "Dormitory Staff",
        Email = "staff@dormitory.com",
        PhoneNumber = "0922222222",
        RoleId = staffRole.RoleId,
        Status = UserStatus.Active,
        CreatedAt = DateTime.UtcNow
    };

    staff.PasswordHash =
        passwordHasher.HashPassword(
            staff,
            "Staff123"
        );

    context.Users.Add(staff);
}

await context.SaveChangesAsync();
    }
}
