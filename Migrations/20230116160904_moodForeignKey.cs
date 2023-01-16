using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api.Migrations
{
    public partial class moodForeignKey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "DailyMoods");

            migrationBuilder.AddColumn<string>(
                name: "EventDate",
                table: "DailyMoods",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddColumn<string>(
                name: "IdentityUserID",
                table: "DailyMoods",
                type: "varchar(255)",
                nullable: true)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_DailyMoods_IdentityUserID",
                table: "DailyMoods",
                column: "IdentityUserID");

            migrationBuilder.AddForeignKey(
                name: "FK_DailyMoods_AspNetUsers_IdentityUserID",
                table: "DailyMoods",
                column: "IdentityUserID",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DailyMoods_AspNetUsers_IdentityUserID",
                table: "DailyMoods");

            migrationBuilder.DropIndex(
                name: "IX_DailyMoods_IdentityUserID",
                table: "DailyMoods");

            migrationBuilder.DropColumn(
                name: "EventDate",
                table: "DailyMoods");

            migrationBuilder.DropColumn(
                name: "IdentityUserID",
                table: "DailyMoods");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "DailyMoods",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }
    }
}
