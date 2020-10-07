using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class QuestionAnswersMig : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EvaluationQuestionAnswer",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    EvaluationId = table.Column<int>(nullable: true),
                    QuestionId = table.Column<int>(nullable: true),
                    AnswerValue = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EvaluationQuestionAnswer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EvaluationQuestionAnswer_Evaluations_EvaluationId",
                        column: x => x.EvaluationId,
                        principalTable: "Evaluations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EvaluationQuestionAnswer_Questions_QuestionId",
                        column: x => x.QuestionId,
                        principalTable: "Questions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionAnswer_EvaluationId",
                table: "EvaluationQuestionAnswer",
                column: "EvaluationId");

            migrationBuilder.CreateIndex(
                name: "IX_EvaluationQuestionAnswer_QuestionId",
                table: "EvaluationQuestionAnswer",
                column: "QuestionId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EvaluationQuestionAnswer");
        }
    }
}
