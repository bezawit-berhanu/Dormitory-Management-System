using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddRegistrarDepartmentId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegistrarDepartmentId",
                table: "Departments",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegistrarDepartmentId",
                table: "Departments");
        }
    }
}
