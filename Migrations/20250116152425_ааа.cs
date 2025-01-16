using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BigBlog.Migrations
{
    /// <inheritdoc />
    public partial class ааа : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Tegs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "UserId",
                value: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Email", "FirstName", "LastName", "Password", "RoleId" },
                values: new object[] { new Guid("00000000-0000-0000-0000-000000000001"), "example@gmail.com", "Админ", "Админ", "123", 3L });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"));

            migrationBuilder.UpdateData(
                table: "Tegs",
                keyColumn: "Id",
                keyValue: new Guid("00000000-0000-0000-0000-000000000001"),
                column: "UserId",
                value: new Guid("00000000-0000-0000-0000-000000000000"));
        }
    }
}
