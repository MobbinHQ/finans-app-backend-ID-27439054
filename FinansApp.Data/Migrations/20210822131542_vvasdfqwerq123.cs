using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace FinansApp.Data.Migrations
{
    public partial class vvasdfqwerq123 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "date",
                table: "Portfoys",
                newName: "Date");

            migrationBuilder.AddColumn<string>(
                name: "Code",
                table: "Portfoys",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateDate",
                table: "Portfoys",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Code",
                table: "Portfoys");

            migrationBuilder.DropColumn(
                name: "CreateDate",
                table: "Portfoys");

            migrationBuilder.RenameColumn(
                name: "Date",
                table: "Portfoys",
                newName: "date");
        }
    }
}
