using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class addPushIdToQueue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EvaluationValue",
                table: "Evaluations");

            migrationBuilder.RenameColumn(
                name: "EvaluationNote",
                table: "Evaluations",
                newName: "Comment");

            migrationBuilder.AddColumn<string>(
                name: "PushId",
                table: "ShiftQueues",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PushId",
                table: "ShiftQueues");

            migrationBuilder.RenameColumn(
                name: "Comment",
                table: "Evaluations",
                newName: "EvaluationNote");

            migrationBuilder.AddColumn<int>(
                name: "EvaluationValue",
                table: "Evaluations",
                nullable: false,
                defaultValue: 0);
        }
    }
}
