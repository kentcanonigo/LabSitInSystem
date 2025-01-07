using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabSitInSystem.Migrations
{
    /// <inheritdoc />
    public partial class removenotifs : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Notifications");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$pRdx.59pzMMdG/ce3NEHfOjlXFnfBMPZ6YgArLB5UTDI1jlFGrsTm");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
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

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$1MtR7ShGeVXtsyzLadADpuruLAlYO/xyL7bY/P0fvNNnd69NT4JlO");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_SitInId",
                table: "Notifications",
                column: "SitInId");
        }
    }
}
