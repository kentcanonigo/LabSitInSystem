using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabSitInSystem.Migrations
{
    /// <inheritdoc />
    public partial class nullabletimein : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIn",
                table: "SitIns",
                type: "TEXT",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "TEXT");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$1MtR7ShGeVXtsyzLadADpuruLAlYO/xyL7bY/P0fvNNnd69NT4JlO");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "TimeIn",
                table: "SitIns",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "TEXT",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$0hphHIt/CA6NQAxMj1fpJevKVVpCzwFH6X964I0xlTblV5jM2xwMy");
        }
    }
}
