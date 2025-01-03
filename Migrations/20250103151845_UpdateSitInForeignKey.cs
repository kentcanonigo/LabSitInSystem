using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LabSitInSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateSitInForeignKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SitIns_Users_StudentUserId",
                table: "SitIns");

            migrationBuilder.DropIndex(
                name: "IX_SitIns_StudentUserId",
                table: "SitIns");

            migrationBuilder.DropColumn(
                name: "StudentUserId",
                table: "SitIns");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$0hphHIt/CA6NQAxMj1fpJevKVVpCzwFH6X964I0xlTblV5jM2xwMy");

            migrationBuilder.CreateIndex(
                name: "IX_SitIns_StudentId",
                table: "SitIns",
                column: "StudentId");

            migrationBuilder.AddForeignKey(
                name: "FK_SitIns_Students_StudentId",
                table: "SitIns",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "StudentId",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SitIns_Students_StudentId",
                table: "SitIns");

            migrationBuilder.DropIndex(
                name: "IX_SitIns_StudentId",
                table: "SitIns");

            migrationBuilder.AddColumn<int>(
                name: "StudentUserId",
                table: "SitIns",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "UserId",
                keyValue: 1,
                column: "PasswordHash",
                value: "$2a$11$cwTpQ/UKnTwGf1JalcNZ6.SqM551uIn.aeMszjdfyMteBjzcBleTm");

            migrationBuilder.CreateIndex(
                name: "IX_SitIns_StudentUserId",
                table: "SitIns",
                column: "StudentUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_SitIns_Users_StudentUserId",
                table: "SitIns",
                column: "StudentUserId",
                principalTable: "Users",
                principalColumn: "UserId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
