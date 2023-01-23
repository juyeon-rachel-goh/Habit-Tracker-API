using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class foreignKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "IdentityUserID",
                table: "DailyHabitRecords",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DailyHabitRecords_IdentityUserID",
                table: "DailyHabitRecords",
                column: "IdentityUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyHabitRecords_AspNetUsers_IdentityUserID",
                table: "DailyHabitRecords",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyHabitRecords_AspNetUsers_IdentityUserID",
                table: "DailyHabitRecords");

            migrationBuilder.DropIndex(
                name: "IX_DailyHabitRecords_IdentityUserID",
                table: "DailyHabitRecords");

            migrationBuilder.DropColumn(
                name: "IdentityUserID",
                table: "DailyHabitRecords");
        }
    }
}
