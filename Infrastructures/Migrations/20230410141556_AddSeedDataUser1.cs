using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructures.Migrations
{
    /// <inheritdoc />
    public partial class AddSeedDataUser1 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "PasswordHash", "Role", "UserName" },
                values: new object[] { 4, new DateTime(2002, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "abcdefghijklmnopqrstuvwxyz", 1, "Lan" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "DateOfBirth", "PasswordHash", "Role", "UserName" },
                values: new object[] { 3, new DateTime(2002, 10, 31, 0, 0, 0, 0, DateTimeKind.Unspecified), "abcdefghijklmnopqrstuvwxyz", 1, "Lan" });
        }
    }
}
