using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class UpdateShiftStartAndEndDataTypeToTimeSpan : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<TimeSpan>(
                name: "Start",
                table: "Shifts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<TimeSpan>(
                name: "End",
                table: "Shifts",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Start",
                table: "Shifts",
                nullable: true,
                oldClrType: typeof(TimeSpan));

            migrationBuilder.AlterColumn<string>(
                name: "End",
                table: "Shifts",
                nullable: true,
                oldClrType: typeof(TimeSpan));
        }
    }
}
