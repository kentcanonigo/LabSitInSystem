using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabSitInSystem.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Students",
                columns: table => new
                {
                    StudentId = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false),
                    Program = table.Column<string>(type: "TEXT", nullable: false),
                    Year = table.Column<string>(type: "TEXT", nullable: false),
                    Section = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Students", x => x.StudentId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Username = table.Column<string>(type: "TEXT", nullable: false),
                    PasswordHash = table.Column<string>(type: "TEXT", nullable: false),
                    FullName = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.UserId);
                });

            migrationBuilder.CreateTable(
                name: "SitIns",
                columns: table => new
                {
                    SitInId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    StudentId = table.Column<string>(type: "TEXT", nullable: false),
                    TimeIn = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TimeOut = table.Column<DateTime>(type: "TEXT", nullable: true),
                    Duration = table.Column<int>(type: "INTEGER", nullable: true),
                    ApprovedByAdmin = table.Column<bool>(type: "INTEGER", nullable: false),
                    StudentUserId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SitIns", x => x.SitInId);
                    table.ForeignKey(
                        name: "FK_SitIns_Users_StudentUserId",
                        column: x => x.StudentUserId,
                        principalTable: "Users",
                        principalColumn: "UserId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    NotificationId = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SitInId = table.Column<int>(type: "INTEGER", nullable: false),
                    NotificationMessage = table.Column<string>(type: "TEXT", nullable: false),
                    NotificationTime = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.NotificationId);
                    table.ForeignKey(
                        name: "FK_Notifications_SitIns_SitInId",
                        column: x => x.SitInId,
                        principalTable: "SitIns",
                        principalColumn: "SitInId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "UserId", "FullName", "PasswordHash", "Username" },
                values: new object[] { 1, "Administrator", "$2a$11$cwTpQ/UKnTwGf1JalcNZ6.SqM551uIn.aeMszjdfyMteBjzcBleTm", "admin@ctu.edu.ph" });

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SitInId",
                table: "Notifications",
                column: "SitInId");

            migrationBuilder.CreateIndex(
                name: "IX_SitIns_StudentUserId",
                table: "SitIns",
                column: "StudentUserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.DropTable(
                name: "Students");

            migrationBuilder.DropTable(
                name: "SitIns");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
