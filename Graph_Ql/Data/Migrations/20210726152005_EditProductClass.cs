using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Graph_Ql.Data.Migrations
{
    public partial class EditProductClass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Description",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<DateTimeOffset>(
                name: "IntroducedAt",
                table: "products",
                type: "datetimeoffset",
                nullable: false,
                defaultValue: new DateTimeOffset(new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), new TimeSpan(0, 0, 0, 0, 0)));

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "Rating",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Stock",
                table: "products",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Type",
                table: "products",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Description",
                table: "products");

            migrationBuilder.DropColumn(
                name: "IntroducedAt",
                table: "products");

            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Rating",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Stock",
                table: "products");

            migrationBuilder.DropColumn(
                name: "Type",
                table: "products");
        }
    }
}
