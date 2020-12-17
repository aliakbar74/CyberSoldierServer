using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class DungeonPositionId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PositionId",
                table: "PlayerDungeons",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PositionId",
                table: "PlayerDungeons");
        }
    }
}
