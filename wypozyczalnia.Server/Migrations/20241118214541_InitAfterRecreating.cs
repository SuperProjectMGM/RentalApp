using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wypozyczalnia.Server.Migrations
{
    /// <inheritdoc />
    public partial class InitAfterRecreating : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Vehicles",
                columns: table => new
                {
                    CarId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    Model = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    SerialNo = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    VinId = table.Column<string>(type: "nvarchar(100)", nullable: false),
                    RegistryNo = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    YearOfProduction = table.Column<int>(type: "int", nullable: false),
                    RentalFrom = table.Column<DateTime>(type: "date", nullable: false),
                    RentalTo = table.Column<DateTime>(type: "date", nullable: false),
                    Prize = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DriveType = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Transmission = table.Column<string>(type: "nvarchar(10)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(500)", nullable: false),
                    Type = table.Column<string>(type: "nvarchar(50)", nullable: false),
                    Rate = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    Localization = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehicles", x => x.CarId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Vehicles");
        }
    }
}
