using Microsoft.EntityFrameworkCore.Migrations;

namespace CyberSoldierServer.Migrations
{
    public partial class ChangeAttakerModelToCampCpu : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attackers_Cpus_CpuId",
                table: "Attackers");

            migrationBuilder.AddForeignKey(
                name: "FK_Attackers_ServerCpus_CpuId",
                table: "Attackers",
                column: "CpuId",
                principalTable: "ServerCpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Attackers_ServerCpus_CpuId",
                table: "Attackers");

            migrationBuilder.AddForeignKey(
                name: "FK_Attackers_Cpus_CpuId",
                table: "Attackers",
                column: "CpuId",
                principalTable: "Cpus",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
