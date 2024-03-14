using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddRoles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "801b0781-ff02-47d2-95db-41a5d51fd1db", "2", "Admin", "Admin" },
                    { "963ef5bb-8c5b-4069-a3fb-9d7b89aa56cb", "1", "Superadmin", "Superadmin" },
                    { "df3d6582-f0fb-42ad-af8d-6d7094c6bde1", "3", "User", "User" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "801b0781-ff02-47d2-95db-41a5d51fd1db");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "963ef5bb-8c5b-4069-a3fb-9d7b89aa56cb");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "df3d6582-f0fb-42ad-af8d-6d7094c6bde1");
        }
    }
}
