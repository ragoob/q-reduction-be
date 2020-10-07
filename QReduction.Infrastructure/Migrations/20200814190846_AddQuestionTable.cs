using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class AddQuestionTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Questions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    QuestionTextAr = table.Column<string>(maxLength: 250, nullable: false),
                    QuestionTextEn = table.Column<string>(maxLength: 250, nullable: false),
                    AnswerValue = table.Column<string>(nullable: true),
                    OrganizationId = table.Column<int>(nullable: false),
                    Code = table.Column<int>(nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Questions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Questions_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Questions_OrganizationId",
                table: "Questions",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Questions");
        }
    }
}
