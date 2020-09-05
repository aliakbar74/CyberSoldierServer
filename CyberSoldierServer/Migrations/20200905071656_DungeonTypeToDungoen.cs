using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class DungeonTypeToDungoen : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "DungeonType",
                table: "Dungeons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DungeonType",
                table: "Dungeons");
        }
    }
}
