using Microsoft.EntityFrameworkCore.Migrations;

namespace University.API.Migrations
{
    public partial class AddSampleData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Start learn cool language", "C# Starter" });

            migrationBuilder.InsertData(
                table: "Courses",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "For angular beginners", "Angular 9" });

            migrationBuilder.InsertData(
                table: "Lessons",
                columns: new[] { "Id", "CourseId", "Description", "Name" },
                values: new object[,]
                {
                    { 1, 1, "Intro", "First" },
                    { 2, 1, "Data types", "Second" },
                    { 3, 2, "Intro", "First" },
                    { 4, 2, "Data types", "Second" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Lessons",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Courses",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
