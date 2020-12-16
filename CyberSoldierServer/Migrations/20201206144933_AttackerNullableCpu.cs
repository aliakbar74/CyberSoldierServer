using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class AttackerNullableCpu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attackers_Cpus_CpuId",
                table: "Attackers");

            migrationBuilder.AlterColumn<int>(
                name: "CpuId",
                table: "Attackers",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Attackers_Cpus_CpuId",
                table: "Attackers",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attackers_Cpus_CpuId",
                table: "Attackers");

            migrationBuilder.AlterColumn<int>(
                name: "CpuId",
                table: "Attackers",
                type: "integer",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Attackers_Cpus_CpuId",
                table: "Attackers",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
