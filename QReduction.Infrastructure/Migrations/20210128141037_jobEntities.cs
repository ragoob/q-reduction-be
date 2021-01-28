using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class jobEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobRequests",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IsDone = table.Column<bool>(nullable: false),
                    JobId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequests_Jobs_JobId",
                        column: x => x.JobId,
                        principalTable: "Jobs",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobRequestParameter",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    Value = table.Column<string>(nullable: true),
                    ValueType = table.Column<string>(nullable: true),
                    JobRequestId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobRequestParameter", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobRequestParameter_JobRequests_JobRequestId",
                        column: x => x.JobRequestId,
                        principalTable: "JobRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Jobs",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "BranchReport" });

            migrationBuilder.CreateIndex(
                name: "IX_JobRequestParameter_JobRequestId",
                table: "JobRequestParameter",
                column: "JobRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_JobRequests_JobId",
                table: "JobRequests",
                column: "JobId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JobRequestParameter");

            migrationBuilder.DropTable(
                name: "JobRequests");

            migrationBuilder.DropTable(
                name: "Jobs");
        }
    }
}
