using Microsoft.EntityFrameworkCore.Migrations;

namespace FinansApp.Data.Migrations
{
    public partial class asdfasdfasdxzcv : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Alerts",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Alerts");
        }
    }
}
