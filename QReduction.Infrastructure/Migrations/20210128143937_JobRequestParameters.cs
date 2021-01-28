using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class JobRequestParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequestParameter_JobRequests_JobRequestId",
                table: "JobRequestParameter");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobRequestParameter",
                table: "JobRequestParameter");

            migrationBuilder.RenameTable(
                name: "JobRequestParameter",
                newName: "JobRequestParameters");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequestParameter_JobRequestId",
                table: "JobRequestParameters",
                newName: "IX_JobRequestParameters_JobRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobRequestParameters",
                table: "JobRequestParameters",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequestParameters_JobRequests_JobRequestId",
                table: "JobRequestParameters",
                column: "JobRequestId",
                principalTable: "JobRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_JobRequestParameters_JobRequests_JobRequestId",
                table: "JobRequestParameters");

            migrationBuilder.DropPrimaryKey(
                name: "PK_JobRequestParameters",
                table: "JobRequestParameters");

            migrationBuilder.RenameTable(
                name: "JobRequestParameters",
                newName: "JobRequestParameter");

            migrationBuilder.RenameIndex(
                name: "IX_JobRequestParameters_JobRequestId",
                table: "JobRequestParameter",
                newName: "IX_JobRequestParameter_JobRequestId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_JobRequestParameter",
                table: "JobRequestParameter",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_JobRequestParameter_JobRequests_JobRequestId",
                table: "JobRequestParameter",
                column: "JobRequestId",
                principalTable: "JobRequests",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
