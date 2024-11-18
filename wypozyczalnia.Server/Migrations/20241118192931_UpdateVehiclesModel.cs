using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wypozyczalnia.Server.Migrations
{
    /// <inheritdoc />
    public partial class UpdateVehiclesModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "YearOfProduction",
                table: "Vehicles",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(4)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalTo",
                table: "Vehicles",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AlterColumn<DateTime>(
                name: "RentalFrom",
                table: "Vehicles",
                type: "date",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)");

            migrationBuilder.AddColumn<string>(
                name: "DriveType",
                table: "Vehicles",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "Localization",
                table: "Vehicles",
                type: "nvarchar(50)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Prize",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<decimal>(
                name: "Rate",
                table: "Vehicles",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "Transmission",
                table: "Vehicles",
                type: "nvarchar(10)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DriveType",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Localization",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Prize",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Rate",
                table: "Vehicles");

            migrationBuilder.DropColumn(
                name: "Transmission",
                table: "Vehicles");

            migrationBuilder.AlterColumn<string>(
                name: "YearOfProduction",
                table: "Vehicles",
                type: "nvarchar(4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "RentalTo",
                table: "Vehicles",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");

            migrationBuilder.AlterColumn<string>(
                name: "RentalFrom",
                table: "Vehicles",
                type: "nvarchar(10)",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "date");
        }
    }
}
