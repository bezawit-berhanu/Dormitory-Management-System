using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddAnnouncementTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Students_StudentId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceAssignments_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceAssignments_Users_UserId",
                table: "MaintenanceAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_StudentId",
                table: "Complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceAssignments",
                table: "MaintenanceAssignments");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Complaints");

            migrationBuilder.RenameTable(
                name: "MaintenanceAssignments",
                newName: "MaintenanceAssignment");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceAssignments_UserId",
                table: "MaintenanceAssignment",
                newName: "IX_MaintenanceAssignment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceAssignments_MaintenanceRequestId",
                table: "MaintenanceAssignment",
                newName: "IX_MaintenanceAssignment_MaintenanceRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceAssignment",
                table: "MaintenanceAssignment",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_SId",
                table: "Complaints",
                column: "SId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Students_SId",
                table: "Complaints",
                column: "SId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceAssignment_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceAssignment",
                column: "MaintenanceRequestId",
                principalTable: "MaintenanceRequests",
                principalColumn: "MaintenanceRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceAssignment_Users_UserId",
                table: "MaintenanceAssignment",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Students_SId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceAssignment_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceAssignment");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceAssignment_Users_UserId",
                table: "MaintenanceAssignment");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_SId",
                table: "Complaints");

            migrationBuilder.DropPrimaryKey(
                name: "PK_MaintenanceAssignment",
                table: "MaintenanceAssignment");

            migrationBuilder.RenameTable(
                name: "MaintenanceAssignment",
                newName: "MaintenanceAssignments");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceAssignment_UserId",
                table: "MaintenanceAssignments",
                newName: "IX_MaintenanceAssignments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_MaintenanceAssignment_MaintenanceRequestId",
                table: "MaintenanceAssignments",
                newName: "IX_MaintenanceAssignments_MaintenanceRequestId");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Complaints",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_MaintenanceAssignments",
                table: "MaintenanceAssignments",
                column: "AssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_StudentId",
                table: "Complaints",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Students_StudentId",
                table: "Complaints",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceAssignments_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceAssignments",
                column: "MaintenanceRequestId",
                principalTable: "MaintenanceRequests",
                principalColumn: "MaintenanceRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceAssignments_Users_UserId",
                table: "MaintenanceAssignments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
