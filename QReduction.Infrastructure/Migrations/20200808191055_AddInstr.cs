using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class AddInstr : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Instructions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Code = table.Column<int>(nullable: false),
                    OrganizationId = table.Column<int>(nullable: true),
                    NameAr = table.Column<string>(maxLength: 50, nullable: false),
                    NameEn = table.Column<string>(maxLength: 50, nullable: false),
                    CreateBy = table.Column<int>(nullable: true),
                    CreateAt = table.Column<DateTime>(type: "smalldatetime", nullable: false),
                    UpdateBy = table.Column<int>(nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedAt = table.Column<DateTime>(type: "smalldatetime", nullable: true),
                    DeletedBy = table.Column<int>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Instructions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Instructions_Organization_OrganizationId",
                        column: x => x.OrganizationId,
                        principalTable: "Organization",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Instructions_OrganizationId",
                table: "Instructions",
                column: "OrganizationId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Instructions");
        }
    }
}
