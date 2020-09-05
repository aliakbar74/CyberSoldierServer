using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class AddWorldToPlayer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds");

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds",
                column: "PlayerId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds");

            migrationBuilder.CreateIndex(
                name: "IX_Worlds_PlayerId",
                table: "Worlds",
                column: "PlayerId");
        }
    }
}
