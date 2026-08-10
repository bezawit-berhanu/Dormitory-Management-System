using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DormitoryManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class CheckCurrentModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTransferRequests",
                table: "RoomTransferRequests");

            migrationBuilder.DropColumn(
                name: "IsApproved",
                table: "RoomTransferResponses");

            migrationBuilder.RenameColumn(
                name: "DateOccurred",
                table: "Violations",
                newName: "ViolationDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Violations",
                newName: "ViolationId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Users",
                newName: "UserId");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "Students",
                newName: "YearOfStudy");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Students",
                newName: "StudentId");

            migrationBuilder.RenameColumn(
                name: "DateReported",
                table: "SecurityIncidents",
                newName: "IncidentDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "SecurityIncidents",
                newName: "IncidentId");

            migrationBuilder.RenameColumn(
                name: "RoomTransferRequestId",
                table: "RoomTransferResponses",
                newName: "TransferRequestId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "RoomTransferResponses",
                newName: "ResponseId");

            migrationBuilder.RenameColumn(
                name: "ToRoomId",
                table: "RoomTransferRequests",
                newName: "RequestedRoomId");

            migrationBuilder.RenameColumn(
                name: "FromRoomId",
                table: "RoomTransferRequests",
                newName: "CurrentRoomId");

            migrationBuilder.RenameColumn(
                name: "BlockId",
                table: "Rooms",
                newName: "FloorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Rooms",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "AssignmentDate",
                table: "RoomAssignments",
                newName: "AssignedDate");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Roles",
                newName: "RoleName");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Roles",
                newName: "RoleId");

            migrationBuilder.RenameColumn(
                name: "Code",
                table: "QRCodes",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DateCreated",
                table: "Notifications",
                newName: "NotificationDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Notifications",
                newName: "NotificationId");

            migrationBuilder.RenameColumn(
                name: "DateRequested",
                table: "MaintenanceRequests",
                newName: "RequestDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MaintenanceRequests",
                newName: "MaintenanceRequestId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "MaintenanceActivities",
                newName: "PerformedByUserUserId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "MaintenanceActivities",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "DateReported",
                table: "MaintenanceActivities",
                newName: "ActivityDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "MaintenanceActivities",
                newName: "ActivityId");

            migrationBuilder.RenameColumn(
                name: "InspectorName",
                table: "Inspections",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Inspections",
                newName: "InspectionId");

            migrationBuilder.RenameColumn(
                name: "FindingDescription",
                table: "InspectionFindings",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "InspectionFindings",
                newName: "FindingId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Floors",
                newName: "FloorId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Dormitories",
                newName: "DormitoryId");

            migrationBuilder.RenameColumn(
                name: "DateFiled",
                table: "Complaints",
                newName: "ComplaintDate");

            migrationBuilder.RenameColumn(
                name: "ResponseText",
                table: "ComplaintResponses",
                newName: "Response");

            migrationBuilder.RenameColumn(
                name: "DateResponded",
                table: "ComplaintResponses",
                newName: "ResponseDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "ComplaintResponses",
                newName: "ResponseId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "CheckOuts",
                newName: "RoomAssignmentId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "CheckIns",
                newName: "RoomAssignmentId");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "Blocks",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Blocks",
                newName: "BlockId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Beds",
                newName: "BedId");

            migrationBuilder.RenameColumn(
                name: "Timestamp",
                table: "AuditLogs",
                newName: "ActionDate");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "AuditLogs",
                newName: "AuditLogId");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Announcements",
                newName: "AnnouncementId");

            migrationBuilder.AddColumn<string>(
                name: "Penalty",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RecordedByUser",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RecordedByUserIdUserId",
                table: "Violations",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "Violations",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ViolationType",
                table: "Violations",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateOfBirth",
                table: "Students",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "EmergencyContactNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "StudentNumber",
                table: "Students",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Students",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "IncidentType",
                table: "SecurityIncidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReportedBy",
                table: "SecurityIncidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ReportedByUserUserId",
                table: "SecurityIncidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomId",
                table: "SecurityIncidents",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "SecurityIncidents",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Decision",
                table: "RoomTransferResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RespondedBy",
                table: "RoomTransferResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RespondedByUserUserId",
                table: "RoomTransferResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "ResponseMessage",
                table: "RoomTransferResponses",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "RoomTransferRequests",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RoomTransferRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .OldAnnotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "TransferRequestId",
                table: "RoomTransferRequests",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "ApprovedBy",
                table: "RoomTransferRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ApprovedByUserUserId",
                table: "RoomTransferRequests",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "RoomTransferRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RoomTransferRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Capacity",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Rooms",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "RoomAssignments",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "AssignedByUserId",
                table: "RoomAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "BedId",
                table: "RoomAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RoomAssignmentId",
                table: "RoomAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "RoomAssignments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "QRCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "GeneratedDate",
                table: "QRCodes",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "QRCodeId",
                table: "QRCodes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "QRCodeValue",
                table: "QRCodes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StudentId",
                table: "QRCodes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsRead",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Notifications",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RequestedBy",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "RequestedByUserUserId",
                table: "MaintenanceRequests",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "MaintenanceRequests",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "ActivityDescription",
                table: "MaintenanceActivities",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "MaintenanceRequestId",
                table: "MaintenanceActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PerformedBy",
                table: "MaintenanceActivities",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "InspectedByUserId",
                table: "Inspections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Remarks",
                table: "Inspections",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Finding",
                table: "InspectionFindings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Recommendation",
                table: "InspectionFindings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Severity",
                table: "InspectionFindings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "Floors",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DormitoryName",
                table: "Dormitories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Location",
                table: "Dormitories",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Complaints",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "ComplaintId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Priority",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Title",
                table: "Complaints",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "RespondedByUserId",
                table: "ComplaintResponses",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "CheckOuts",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CheckOutId",
                table: "CheckOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CheckedOutBy",
                table: "CheckOuts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CheckedOutByUserUserId",
                table: "CheckOuts",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Reason",
                table: "CheckOuts",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "CheckIns",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CheckInId",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CheckedInByUserId",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "BlockName",
                table: "Blocks",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "DormitoryId",
                table: "Blocks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Beds",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<int>(
                name: "UserId",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "RecordId",
                table: "AuditLogs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "TableName",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "CreatedBy",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CreatedByUserUserId",
                table: "Announcements",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "ExpiryDate",
                table: "Announcements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "PublishedDate",
                table: "Announcements",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Announcements",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTransferRequests",
                table: "RoomTransferRequests",
                column: "TransferRequestId");

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentName = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentId);
                });

            migrationBuilder.CreateTable(
                name: "MaintenanceAssignments",
                columns: table => new
                {
                    AssignmentId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MaintenanceRequestId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    AssignedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaintenanceAssignments", x => x.AssignmentId);
                    table.ForeignKey(
                        name: "FK_MaintenanceAssignments_MaintenanceRequests_MaintenanceRequestId",
                        column: x => x.MaintenanceRequestId,
                        principalTable: "MaintenanceRequests",
                        principalColumn: "MaintenanceRequestId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MaintenanceAssignments_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Violations_RecordedByUserIdUserId",
                table: "Violations",
                column: "RecordedByUserIdUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Violations_StudentId",
                table: "Violations",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Students_DepartmentId",
                table: "Students",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityIncidents_ReportedByUserUserId",
                table: "SecurityIncidents",
                column: "ReportedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_SecurityIncidents_RoomId",
                table: "SecurityIncidents",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferResponses_RespondedByUserUserId",
                table: "RoomTransferResponses",
                column: "RespondedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferResponses_TransferRequestId",
                table: "RoomTransferResponses",
                column: "TransferRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequests_ApprovedByUserUserId",
                table: "RoomTransferRequests",
                column: "ApprovedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequests_CurrentRoomId",
                table: "RoomTransferRequests",
                column: "CurrentRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequests_RequestedRoomId",
                table: "RoomTransferRequests",
                column: "RequestedRoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomTransferRequests_StudentId",
                table: "RoomTransferRequests",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms",
                column: "FloorId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAssignments_AssignedByUserId",
                table: "RoomAssignments",
                column: "AssignedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAssignments_BedId",
                table: "RoomAssignments",
                column: "BedId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAssignments_RoomId",
                table: "RoomAssignments",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_RoomAssignments_StudentId",
                table: "RoomAssignments",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_QRCodes_StudentId",
                table: "QRCodes",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_RequestedByUserUserId",
                table: "MaintenanceRequests",
                column: "RequestedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceRequests_RoomId",
                table: "MaintenanceRequests",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_MaintenanceRequestId",
                table: "MaintenanceActivities",
                column: "MaintenanceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceActivities_PerformedByUserUserId",
                table: "MaintenanceActivities",
                column: "PerformedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_InspectedByUserId",
                table: "Inspections",
                column: "InspectedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Inspections_RoomId",
                table: "Inspections",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_InspectionFindings_InspectionId",
                table: "InspectionFindings",
                column: "InspectionId");

            migrationBuilder.CreateIndex(
                name: "IX_Floors_BlockId",
                table: "Floors",
                column: "BlockId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaints_StudentId",
                table: "Complaints",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintResponses_ComplaintId",
                table: "ComplaintResponses",
                column: "ComplaintId");

            migrationBuilder.CreateIndex(
                name: "IX_ComplaintResponses_RespondedByUserId",
                table: "ComplaintResponses",
                column: "RespondedByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_CheckedOutByUserUserId",
                table: "CheckOuts",
                column: "CheckedOutByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_RoomAssignmentId",
                table: "CheckOuts",
                column: "RoomAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_StudentId",
                table: "CheckOuts",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_CheckedInByUserId",
                table: "CheckIns",
                column: "CheckedInByUserId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_RoomAssignmentId",
                table: "CheckIns",
                column: "RoomAssignmentId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_StudentId",
                table: "CheckIns",
                column: "StudentId");

            migrationBuilder.CreateIndex(
                name: "IX_Blocks_DormitoryId",
                table: "Blocks",
                column: "DormitoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Beds_RoomId",
                table: "Beds",
                column: "RoomId");

            migrationBuilder.CreateIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Announcements_CreatedByUserUserId",
                table: "Announcements",
                column: "CreatedByUserUserId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceAssignments_MaintenanceRequestId",
                table: "MaintenanceAssignments",
                column: "MaintenanceRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_MaintenanceAssignments_UserId",
                table: "MaintenanceAssignments",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Announcements_Users_CreatedByUserUserId",
                table: "Announcements",
                column: "CreatedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                table: "AuditLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Beds_Rooms_RoomId",
                table: "Beds",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Blocks_Dormitories_DormitoryId",
                table: "Blocks",
                column: "DormitoryId",
                principalTable: "Dormitories",
                principalColumn: "DormitoryId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_RoomAssignments_RoomAssignmentId",
                table: "CheckIns",
                column: "RoomAssignmentId",
                principalTable: "RoomAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Students_StudentId",
                table: "CheckIns",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Users_CheckedInByUserId",
                table: "CheckIns",
                column: "CheckedInByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_RoomAssignments_RoomAssignmentId",
                table: "CheckOuts",
                column: "RoomAssignmentId",
                principalTable: "RoomAssignments",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Students_StudentId",
                table: "CheckOuts",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Users_CheckedOutByUserUserId",
                table: "CheckOuts",
                column: "CheckedOutByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintResponses_Complaints_ComplaintId",
                table: "ComplaintResponses",
                column: "ComplaintId",
                principalTable: "Complaints",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ComplaintResponses_Users_RespondedByUserId",
                table: "ComplaintResponses",
                column: "RespondedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Complaints_Students_StudentId",
                table: "Complaints",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Floors_Blocks_BlockId",
                table: "Floors",
                column: "BlockId",
                principalTable: "Blocks",
                principalColumn: "BlockId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InspectionFindings_Inspections_InspectionId",
                table: "InspectionFindings",
                column: "InspectionId",
                principalTable: "Inspections",
                principalColumn: "InspectionId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_Rooms_RoomId",
                table: "Inspections",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Inspections_Users_InspectedByUserId",
                table: "Inspections",
                column: "InspectedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceActivities_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceActivities",
                column: "MaintenanceRequestId",
                principalTable: "MaintenanceRequests",
                principalColumn: "MaintenanceRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceActivities_Users_PerformedByUserUserId",
                table: "MaintenanceActivities",
                column: "PerformedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Rooms_RoomId",
                table: "MaintenanceRequests",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_MaintenanceRequests_Users_RequestedByUserUserId",
                table: "MaintenanceRequests",
                column: "RequestedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QRCodes_Students_StudentId",
                table: "QRCodes",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAssignments_Beds_BedId",
                table: "RoomAssignments",
                column: "BedId",
                principalTable: "Beds",
                principalColumn: "BedId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAssignments_Rooms_RoomId",
                table: "RoomAssignments",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAssignments_Students_StudentId",
                table: "RoomAssignments",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomAssignments_Users_AssignedByUserId",
                table: "RoomAssignments",
                column: "AssignedByUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Rooms_Floors_FloorId",
                table: "Rooms",
                column: "FloorId",
                principalTable: "Floors",
                principalColumn: "FloorId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferRequests_Rooms_CurrentRoomId",
                table: "RoomTransferRequests",
                column: "CurrentRoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferRequests_Rooms_RequestedRoomId",
                table: "RoomTransferRequests",
                column: "RequestedRoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferRequests_Students_StudentId",
                table: "RoomTransferRequests",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferRequests_Users_ApprovedByUserUserId",
                table: "RoomTransferRequests",
                column: "ApprovedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferResponses_RoomTransferRequests_TransferRequestId",
                table: "RoomTransferResponses",
                column: "TransferRequestId",
                principalTable: "RoomTransferRequests",
                principalColumn: "TransferRequestId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_RoomTransferResponses_Users_RespondedByUserUserId",
                table: "RoomTransferResponses",
                column: "RespondedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityIncidents_Rooms_RoomId",
                table: "SecurityIncidents",
                column: "RoomId",
                principalTable: "Rooms",
                principalColumn: "RoomId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SecurityIncidents_Users_ReportedByUserUserId",
                table: "SecurityIncidents",
                column: "ReportedByUserUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students",
                column: "DepartmentId",
                principalTable: "Departments",
                principalColumn: "DepartmentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_Students_StudentId",
                table: "Violations",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Violations_Users_RecordedByUserIdUserId",
                table: "Violations",
                column: "RecordedByUserIdUserId",
                principalTable: "Users",
                principalColumn: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Announcements_Users_CreatedByUserUserId",
                table: "Announcements");

            migrationBuilder.DropForeignKey(
                name: "FK_AuditLogs_Users_UserId",
                table: "AuditLogs");

            migrationBuilder.DropForeignKey(
                name: "FK_Beds_Rooms_RoomId",
                table: "Beds");

            migrationBuilder.DropForeignKey(
                name: "FK_Blocks_Dormitories_DormitoryId",
                table: "Blocks");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_RoomAssignments_RoomAssignmentId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Students_StudentId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Users_CheckedInByUserId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_RoomAssignments_RoomAssignmentId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Students_StudentId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Users_CheckedOutByUserUserId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintResponses_Complaints_ComplaintId",
                table: "ComplaintResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_ComplaintResponses_Users_RespondedByUserId",
                table: "ComplaintResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_Complaints_Students_StudentId",
                table: "Complaints");

            migrationBuilder.DropForeignKey(
                name: "FK_Floors_Blocks_BlockId",
                table: "Floors");

            migrationBuilder.DropForeignKey(
                name: "FK_InspectionFindings_Inspections_InspectionId",
                table: "InspectionFindings");

            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_Rooms_RoomId",
                table: "Inspections");

            migrationBuilder.DropForeignKey(
                name: "FK_Inspections_Users_InspectedByUserId",
                table: "Inspections");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceActivities_MaintenanceRequests_MaintenanceRequestId",
                table: "MaintenanceActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceActivities_Users_PerformedByUserUserId",
                table: "MaintenanceActivities");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Rooms_RoomId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_MaintenanceRequests_Users_RequestedByUserUserId",
                table: "MaintenanceRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_QRCodes_Students_StudentId",
                table: "QRCodes");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAssignments_Beds_BedId",
                table: "RoomAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAssignments_Rooms_RoomId",
                table: "RoomAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAssignments_Students_StudentId",
                table: "RoomAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomAssignments_Users_AssignedByUserId",
                table: "RoomAssignments");

            migrationBuilder.DropForeignKey(
                name: "FK_Rooms_Floors_FloorId",
                table: "Rooms");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferRequests_Rooms_CurrentRoomId",
                table: "RoomTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferRequests_Rooms_RequestedRoomId",
                table: "RoomTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferRequests_Students_StudentId",
                table: "RoomTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferRequests_Users_ApprovedByUserUserId",
                table: "RoomTransferRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferResponses_RoomTransferRequests_TransferRequestId",
                table: "RoomTransferResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_RoomTransferResponses_Users_RespondedByUserUserId",
                table: "RoomTransferResponses");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityIncidents_Rooms_RoomId",
                table: "SecurityIncidents");

            migrationBuilder.DropForeignKey(
                name: "FK_SecurityIncidents_Users_ReportedByUserUserId",
                table: "SecurityIncidents");

            migrationBuilder.DropForeignKey(
                name: "FK_Students_Departments_DepartmentId",
                table: "Students");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_Students_StudentId",
                table: "Violations");

            migrationBuilder.DropForeignKey(
                name: "FK_Violations_Users_RecordedByUserIdUserId",
                table: "Violations");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "MaintenanceAssignments");

            migrationBuilder.DropIndex(
                name: "IX_Violations_RecordedByUserIdUserId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_Violations_StudentId",
                table: "Violations");

            migrationBuilder.DropIndex(
                name: "IX_Students_DepartmentId",
                table: "Students");

            migrationBuilder.DropIndex(
                name: "IX_SecurityIncidents_ReportedByUserUserId",
                table: "SecurityIncidents");

            migrationBuilder.DropIndex(
                name: "IX_SecurityIncidents_RoomId",
                table: "SecurityIncidents");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferResponses_RespondedByUserUserId",
                table: "RoomTransferResponses");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferResponses_TransferRequestId",
                table: "RoomTransferResponses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_RoomTransferRequests",
                table: "RoomTransferRequests");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferRequests_ApprovedByUserUserId",
                table: "RoomTransferRequests");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferRequests_CurrentRoomId",
                table: "RoomTransferRequests");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferRequests_RequestedRoomId",
                table: "RoomTransferRequests");

            migrationBuilder.DropIndex(
                name: "IX_RoomTransferRequests_StudentId",
                table: "RoomTransferRequests");

            migrationBuilder.DropIndex(
                name: "IX_Rooms_FloorId",
                table: "Rooms");

            migrationBuilder.DropIndex(
                name: "IX_RoomAssignments_AssignedByUserId",
                table: "RoomAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RoomAssignments_BedId",
                table: "RoomAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RoomAssignments_RoomId",
                table: "RoomAssignments");

            migrationBuilder.DropIndex(
                name: "IX_RoomAssignments_StudentId",
                table: "RoomAssignments");

            migrationBuilder.DropIndex(
                name: "IX_QRCodes_StudentId",
                table: "QRCodes");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_RequestedByUserUserId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceRequests_RoomId",
                table: "MaintenanceRequests");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceActivities_MaintenanceRequestId",
                table: "MaintenanceActivities");

            migrationBuilder.DropIndex(
                name: "IX_MaintenanceActivities_PerformedByUserUserId",
                table: "MaintenanceActivities");

            migrationBuilder.DropIndex(
                name: "IX_Inspections_InspectedByUserId",
                table: "Inspections");

            migrationBuilder.DropIndex(
                name: "IX_Inspections_RoomId",
                table: "Inspections");

            migrationBuilder.DropIndex(
                name: "IX_InspectionFindings_InspectionId",
                table: "InspectionFindings");

            migrationBuilder.DropIndex(
                name: "IX_Floors_BlockId",
                table: "Floors");

            migrationBuilder.DropIndex(
                name: "IX_Complaints_StudentId",
                table: "Complaints");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintResponses_ComplaintId",
                table: "ComplaintResponses");

            migrationBuilder.DropIndex(
                name: "IX_ComplaintResponses_RespondedByUserId",
                table: "ComplaintResponses");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_CheckedOutByUserUserId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_RoomAssignmentId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_StudentId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_CheckedInByUserId",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_RoomAssignmentId",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_StudentId",
                table: "CheckIns");

            migrationBuilder.DropIndex(
                name: "IX_Blocks_DormitoryId",
                table: "Blocks");

            migrationBuilder.DropIndex(
                name: "IX_Beds_RoomId",
                table: "Beds");

            migrationBuilder.DropIndex(
                name: "IX_AuditLogs_UserId",
                table: "AuditLogs");

            migrationBuilder.DropIndex(
                name: "IX_Announcements_CreatedByUserUserId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Penalty",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "RecordedByUser",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "RecordedByUserIdUserId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "ViolationType",
                table: "Violations");

            migrationBuilder.DropColumn(
                name: "DateOfBirth",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "EmergencyContactNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "StudentNumber",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Students");

            migrationBuilder.DropColumn(
                name: "IncidentType",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "ReportedBy",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "ReportedByUserUserId",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "RoomId",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "SecurityIncidents");

            migrationBuilder.DropColumn(
                name: "Decision",
                table: "RoomTransferResponses");

            migrationBuilder.DropColumn(
                name: "RespondedBy",
                table: "RoomTransferResponses");

            migrationBuilder.DropColumn(
                name: "RespondedByUserUserId",
                table: "RoomTransferResponses");

            migrationBuilder.DropColumn(
                name: "ResponseMessage",
                table: "RoomTransferResponses");

            migrationBuilder.DropColumn(
                name: "TransferRequestId",
                table: "RoomTransferRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedBy",
                table: "RoomTransferRequests");

            migrationBuilder.DropColumn(
                name: "ApprovedByUserUserId",
                table: "RoomTransferRequests");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "RoomTransferRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RoomTransferRequests");

            migrationBuilder.DropColumn(
                name: "Capacity",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Rooms");

            migrationBuilder.DropColumn(
                name: "AssignedByUserId",
                table: "RoomAssignments");

            migrationBuilder.DropColumn(
                name: "BedId",
                table: "RoomAssignments");

            migrationBuilder.DropColumn(
                name: "RoomAssignmentId",
                table: "RoomAssignments");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "RoomAssignments");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "QRCodes");

            migrationBuilder.DropColumn(
                name: "GeneratedDate",
                table: "QRCodes");

            migrationBuilder.DropColumn(
                name: "QRCodeId",
                table: "QRCodes");

            migrationBuilder.DropColumn(
                name: "QRCodeValue",
                table: "QRCodes");

            migrationBuilder.DropColumn(
                name: "StudentId",
                table: "QRCodes");

            migrationBuilder.DropColumn(
                name: "IsRead",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "RequestedBy",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "RequestedByUserUserId",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "MaintenanceRequests");

            migrationBuilder.DropColumn(
                name: "ActivityDescription",
                table: "MaintenanceActivities");

            migrationBuilder.DropColumn(
                name: "MaintenanceRequestId",
                table: "MaintenanceActivities");

            migrationBuilder.DropColumn(
                name: "PerformedBy",
                table: "MaintenanceActivities");

            migrationBuilder.DropColumn(
                name: "InspectedByUserId",
                table: "Inspections");

            migrationBuilder.DropColumn(
                name: "Remarks",
                table: "Inspections");

            migrationBuilder.DropColumn(
                name: "Finding",
                table: "InspectionFindings");

            migrationBuilder.DropColumn(
                name: "Recommendation",
                table: "InspectionFindings");

            migrationBuilder.DropColumn(
                name: "Severity",
                table: "InspectionFindings");

            migrationBuilder.DropColumn(
                name: "Description",
                table: "Floors");

            migrationBuilder.DropColumn(
                name: "DormitoryName",
                table: "Dormitories");

            migrationBuilder.DropColumn(
                name: "Location",
                table: "Dormitories");

            migrationBuilder.DropColumn(
                name: "ComplaintId",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Priority",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "Title",
                table: "Complaints");

            migrationBuilder.DropColumn(
                name: "RespondedByUserId",
                table: "ComplaintResponses");

            migrationBuilder.DropColumn(
                name: "CheckOutId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "CheckedOutBy",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "CheckedOutByUserUserId",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "Reason",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "CheckInId",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "CheckedInByUserId",
                table: "CheckIns");

            migrationBuilder.DropColumn(
                name: "BlockName",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "DormitoryId",
                table: "Blocks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Beds");

            migrationBuilder.DropColumn(
                name: "RecordId",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "TableName",
                table: "AuditLogs");

            migrationBuilder.DropColumn(
                name: "CreatedBy",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "CreatedByUserUserId",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "ExpiryDate",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Message",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "PublishedDate",
                table: "Announcements");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Announcements");

            migrationBuilder.RenameColumn(
                name: "ViolationDate",
                table: "Violations",
                newName: "DateOccurred");

            migrationBuilder.RenameColumn(
                name: "ViolationId",
                table: "Violations",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Users",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "YearOfStudy",
                table: "Students",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "StudentId",
                table: "Students",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "IncidentDate",
                table: "SecurityIncidents",
                newName: "DateReported");

            migrationBuilder.RenameColumn(
                name: "IncidentId",
                table: "SecurityIncidents",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "TransferRequestId",
                table: "RoomTransferResponses",
                newName: "RoomTransferRequestId");

            migrationBuilder.RenameColumn(
                name: "ResponseId",
                table: "RoomTransferResponses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RequestedRoomId",
                table: "RoomTransferRequests",
                newName: "ToRoomId");

            migrationBuilder.RenameColumn(
                name: "CurrentRoomId",
                table: "RoomTransferRequests",
                newName: "FromRoomId");

            migrationBuilder.RenameColumn(
                name: "FloorId",
                table: "Rooms",
                newName: "BlockId");

            migrationBuilder.RenameColumn(
                name: "RoomId",
                table: "Rooms",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AssignedDate",
                table: "RoomAssignments",
                newName: "AssignmentDate");

            migrationBuilder.RenameColumn(
                name: "RoleName",
                table: "Roles",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "RoleId",
                table: "Roles",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "QRCodes",
                newName: "Code");

            migrationBuilder.RenameColumn(
                name: "NotificationDate",
                table: "Notifications",
                newName: "DateCreated");

            migrationBuilder.RenameColumn(
                name: "NotificationId",
                table: "Notifications",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RequestDate",
                table: "MaintenanceRequests",
                newName: "DateRequested");

            migrationBuilder.RenameColumn(
                name: "MaintenanceRequestId",
                table: "MaintenanceRequests",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "MaintenanceActivities",
                newName: "Description");

            migrationBuilder.RenameColumn(
                name: "PerformedByUserUserId",
                table: "MaintenanceActivities",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "ActivityDate",
                table: "MaintenanceActivities",
                newName: "DateReported");

            migrationBuilder.RenameColumn(
                name: "ActivityId",
                table: "MaintenanceActivities",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "Inspections",
                newName: "InspectorName");

            migrationBuilder.RenameColumn(
                name: "InspectionId",
                table: "Inspections",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "InspectionFindings",
                newName: "FindingDescription");

            migrationBuilder.RenameColumn(
                name: "FindingId",
                table: "InspectionFindings",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "FloorId",
                table: "Floors",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "DormitoryId",
                table: "Dormitories",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ComplaintDate",
                table: "Complaints",
                newName: "DateFiled");

            migrationBuilder.RenameColumn(
                name: "ResponseDate",
                table: "ComplaintResponses",
                newName: "DateResponded");

            migrationBuilder.RenameColumn(
                name: "Response",
                table: "ComplaintResponses",
                newName: "ResponseText");

            migrationBuilder.RenameColumn(
                name: "ResponseId",
                table: "ComplaintResponses",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "RoomAssignmentId",
                table: "CheckOuts",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "RoomAssignmentId",
                table: "CheckIns",
                newName: "RoomId");

            migrationBuilder.RenameColumn(
                name: "Description",
                table: "Blocks",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "BlockId",
                table: "Blocks",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "BedId",
                table: "Beds",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "ActionDate",
                table: "AuditLogs",
                newName: "Timestamp");

            migrationBuilder.RenameColumn(
                name: "AuditLogId",
                table: "AuditLogs",
                newName: "Id");

            migrationBuilder.RenameColumn(
                name: "AnnouncementId",
                table: "Announcements",
                newName: "Id");

            migrationBuilder.AddColumn<bool>(
                name: "IsApproved",
                table: "RoomTransferResponses",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "RoomTransferRequests",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "RoomTransferRequests",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "RoomAssignments",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "Complaints",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "CheckOuts",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "CheckIns",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                table: "AuditLogs",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddPrimaryKey(
                name: "PK_RoomTransferRequests",
                table: "RoomTransferRequests",
                column: "Id");
        }
    }
}
