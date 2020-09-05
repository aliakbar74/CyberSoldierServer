using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class ChangeCpuPowerToLevel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Power",
                table: "Cpus");

            migrationBuilder.AddColumn<int>(
                name: "Level",
                table: "Cpus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Level",
                table: "Cpus");

            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Cpus",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
