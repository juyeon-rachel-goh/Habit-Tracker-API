using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class HabitStructureChange : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IconImage",
                table: "Habits");

            migrationBuilder.DropColumn(
                name: "CompletionStatus",
                table: "DailyHabitRecords");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IconImage",
                table: "Habits",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<bool>(
                name: "CompletionStatus",
                table: "DailyHabitRecords",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: false);
        }
    }
}
