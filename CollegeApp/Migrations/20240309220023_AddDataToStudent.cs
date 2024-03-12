using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace CollegeApp.Migrations
{
    /// <inheritdoc />
    public partial class AddDataToStudent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "students",
                columns: new[] { "Id", "Adress", "DOB", "Email", "StudentName" },
                values: new object[,]
                {
                    { 1, "Nablus", new DateTime(2022, 12, 12, 0, 0, 0, 0, DateTimeKind.Unspecified), "m@gmail.com", "Mohamad" },
                    { 2, "Jenin", new DateTime(2022, 4, 4, 0, 0, 0, 0, DateTimeKind.Unspecified), "f@gmail.com", "Faiq" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "students",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
