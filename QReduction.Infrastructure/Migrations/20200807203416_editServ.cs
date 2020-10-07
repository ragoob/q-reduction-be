using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class editServ : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "ShiftUsers",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_ShiftUsers_ServiceId",
                table: "ShiftUsers",
                column: "ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShiftUsers_Services_ServiceId",
                table: "ShiftUsers",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShiftUsers_Services_ServiceId",
                table: "ShiftUsers");

            migrationBuilder.DropIndex(
                name: "IX_ShiftUsers_ServiceId",
                table: "ShiftUsers");

            migrationBuilder.DropColumn(
                name: "ServiceId",
                table: "ShiftUsers");
        }
    }
}
