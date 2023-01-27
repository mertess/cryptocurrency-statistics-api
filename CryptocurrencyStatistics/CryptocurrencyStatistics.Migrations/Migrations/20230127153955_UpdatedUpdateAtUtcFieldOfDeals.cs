using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace CryptocurrencyStatistics.Migrations.Migrations
{
    public partial class UpdatedUpdateAtUtcFieldOfDeals : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAtUtc",
                table: "Deals",
                nullable: false,
                oldClrType: typeof(DateTimeOffset),
                oldType: "timestamp with time zone");

            migrationBuilder.AlterColumn<string>(
                name: "Currencies",
                table: "Deals",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<DateTimeOffset>(
                name: "UpdatedAtUtc",
                table: "Deals",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime));

            migrationBuilder.AlterColumn<string>(
                name: "Currencies",
                table: "Deals",
                type: "text",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
