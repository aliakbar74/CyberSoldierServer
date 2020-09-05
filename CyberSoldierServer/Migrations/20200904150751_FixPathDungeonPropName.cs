using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class FixPathDungeonPropName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathDungeons_WorldPaths_WorldPathId",
                table: "PathDungeons");

            migrationBuilder.DropColumn(
                name: "PathId",
                table: "PathDungeons");

            migrationBuilder.AlterColumn<int>(
                name: "WorldPathId",
                table: "PathDungeons",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_PathDungeons_WorldPaths_WorldPathId",
                table: "PathDungeons",
                column: "WorldPathId",
                principalTable: "WorldPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PathDungeons_WorldPaths_WorldPathId",
                table: "PathDungeons");

            migrationBuilder.AlterColumn<int>(
                name: "WorldPathId",
                table: "PathDungeons",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<int>(
                name: "PathId",
                table: "PathDungeons",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_PathDungeons_WorldPaths_WorldPathId",
                table: "PathDungeons",
                column: "WorldPathId",
                principalTable: "WorldPaths",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
