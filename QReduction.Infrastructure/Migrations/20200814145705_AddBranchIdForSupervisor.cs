using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class AddBranchIdForSupervisor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "BranchId",
                schema: "Acl",
                table: "Users",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "NoteEN",
                table: "Services",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Users_BranchId",
                schema: "Acl",
                table: "Users",
                column: "BranchId");

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Branchs_BranchId",
                schema: "Acl",
                table: "Users",
                column: "BranchId",
                principalTable: "Branchs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Users_Branchs_BranchId",
                schema: "Acl",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Users_BranchId",
                schema: "Acl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "BranchId",
                schema: "Acl",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "NoteEN",
                table: "Services");
        }
    }
}
