using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class AddCpuTypeToCpu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CpuType",
                table: "Cpus",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CpuType",
                table: "Cpus");
        }
    }
}
