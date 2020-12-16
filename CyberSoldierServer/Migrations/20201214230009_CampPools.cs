using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CyberSoldierServer.Migrations
{
    public partial class CampPools : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "TokenPacks",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Capacity",
                table: "Servers",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Token",
                table: "Players",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AlterColumn<int>(
                name: "Gem",
                table: "Players",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.AddColumn<DateTime>(
                name: "LastCollectTime",
                table: "PlayerCamps",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "GemPacks",
                nullable: false,
                oldClrType: typeof(long),
                oldType: "bigint");

            migrationBuilder.CreateTable(
                name: "Pools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Capacity = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pools", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CampPools",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    CurrentValue = table.Column<int>(nullable: false),
                    PoolId = table.Column<int>(nullable: false),
                    CampId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampPools", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CampPools_PlayerCamps_CampId",
                        column: x => x.CampId,
                        principalTable: "PlayerCamps",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CampPools_Pools_PoolId",
                        column: x => x.PoolId,
                        principalTable: "Pools",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CampPools_CampId",
                table: "CampPools",
                column: "CampId");

            migrationBuilder.CreateIndex(
                name: "IX_CampPools_PoolId",
                table: "CampPools",
                column: "PoolId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CampPools");

            migrationBuilder.DropTable(
                name: "Pools");

            migrationBuilder.DropColumn(
                name: "LastCollectTime",
                table: "PlayerCamps");

            migrationBuilder.AlterColumn<long>(
                name: "Value",
                table: "TokenPacks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "Capacity",
                table: "Servers",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "Token",
                table: "Players",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "Gem",
                table: "Players",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<long>(
                name: "Value",
                table: "GemPacks",
                type: "bigint",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
