using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class FixPlayerWorldRelMigrate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds");

            migrationBuilder.DropColumn(
                name: "WorldId",
                table: "Players");

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds");

            migrationBuilder.AddColumn<int>(
                name: "WorldId",
                table: "Players",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds",
                column: "PlayerId",
                unique: true);
        }
    }
}
