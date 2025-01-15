using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wypozyczalnia.Server.Migrations
{
    /// <inheritdoc />
    public partial class AdditionReturnInformationInRental : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "EmployeeId",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "Rentals",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.AddColumn<string>(
                name: "ReturnClientDescription",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ReturnEmployeeDescription",
                table: "Rentals",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ReturnLatitude",
                table: "Rentals",
                type: "real",
                nullable: true);

            migrationBuilder.AddColumn<float>(
                name: "ReturnLongtitude",
                table: "Rentals",
                type: "real",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "EmployeeId",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "Price",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnClientDescription",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnEmployeeDescription",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnLatitude",
                table: "Rentals");

            migrationBuilder.DropColumn(
                name: "ReturnLongtitude",
                table: "Rentals");
        }
    }
}
