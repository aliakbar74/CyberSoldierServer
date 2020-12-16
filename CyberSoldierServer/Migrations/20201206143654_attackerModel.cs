using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace CyberSoldierServer.Migrations
{
    public partial class attackerModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Power",
                table: "Cpus",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Attackers",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(nullable: false),
                    AttackerPlayerId = table.Column<int>(nullable: false),
                    DungeonCount = table.Column<int>(nullable: false),
                    CpuId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attackers", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attackers_Cpus_CpuId",
                        column: x => x.CpuId,
                        principalTable: "Cpus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attackers_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Attackers_CpuId",
                table: "Attackers",
                column: "CpuId");

            migrationBuilder.CreateIndex(
                name: "IX_Attackers_PlayerId",
                table: "Attackers",
                column: "PlayerId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Attackers");

            migrationBuilder.DropColumn(
                name: "Power",
                table: "Cpus");
        }
    }
}
