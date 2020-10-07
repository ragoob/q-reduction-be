using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class changeAbout : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "AboutTextEn",
                table: "About",
                newName: "LabelValueEn");

            migrationBuilder.RenameColumn(
                name: "AboutTextAr",
                table: "About",
                newName: "LabelValueAr");

            migrationBuilder.AddColumn<string>(
                name: "LabelTextAr",
                table: "About",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LabelTextEn",
                table: "About",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LabelTextAr",
                table: "About");

            migrationBuilder.DropColumn(
                name: "LabelTextEn",
                table: "About");

            migrationBuilder.RenameColumn(
                name: "LabelValueEn",
                table: "About",
                newName: "AboutTextEn");

            migrationBuilder.RenameColumn(
                name: "LabelValueAr",
                table: "About",
                newName: "AboutTextAr");
        }
    }
}
