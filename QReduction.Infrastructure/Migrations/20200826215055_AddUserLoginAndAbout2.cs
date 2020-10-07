using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace QReduction.Infrastructure.Migrations
{
    public partial class AddUserLoginAndAbout2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "HelpAndSupport",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreateBy",
                table: "HelpAndSupport",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "HelpAndSupport",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "HelpAndSupport",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "HelpAndSupport",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "HelpAndSupport",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "HelpAndSupport",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "CreateAt",
                table: "About",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<int>(
                name: "CreateBy",
                table: "About",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletedAt",
                table: "About",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "DeletedBy",
                table: "About",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "About",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdateAt",
                table: "About",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "UpdateBy",
                table: "About",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "HelpAndSupport");

            migrationBuilder.DropColumn(
                name: "CreateAt",
                table: "About");

            migrationBuilder.DropColumn(
                name: "CreateBy",
                table: "About");

            migrationBuilder.DropColumn(
                name: "DeletedAt",
                table: "About");

            migrationBuilder.DropColumn(
                name: "DeletedBy",
                table: "About");

            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "About");

            migrationBuilder.DropColumn(
                name: "UpdateAt",
                table: "About");

            migrationBuilder.DropColumn(
                name: "UpdateBy",
                table: "About");
        }
    }
}
