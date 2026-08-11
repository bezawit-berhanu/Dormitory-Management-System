using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixAnnouncementForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_CreatedByUserUserId",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_CreatedByUserUserId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Announcements");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatedBy",
                table: "Announcements",
                column: "CreatedBy");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_CreatedBy",
                table: "Announcements",
                column: "CreatedBy",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_CreatedBy",
                table: "Announcements");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_CreatedBy",
                table: "Announcements");

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatedByUserUserId",
                table: "Announcements",
                column: "CreatedByUserUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_CreatedByUserUserId",
                table: "Announcements",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
