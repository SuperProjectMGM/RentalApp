using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace wypozyczalnia.Server.Migrations.Rentals
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ClientInfo",
                columns: table => new
                {
                    ClientInfoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BirthDate = table.Column<DateOnly>(type: "date", nullable: false),
                    LicenseNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DrivingLicenseIssueDate = table.Column<DateOnly>(type: "date", nullable: false),
                    PersonalNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    City = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StreetAndNumber = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PostalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ClientInfo", x => x.ClientInfoId);
                });

            migrationBuilder.CreateTable(
                name: "Rentals",
                columns: table => new
                {
                    RentalId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UserInfoClientInfoId = table.Column<int>(type: "int", nullable: false),
                    Vin = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Rentals", x => x.RentalId);
                    table.ForeignKey(
                        name: "FK_Rentals_ClientInfo_UserInfoClientInfoId",
                        column: x => x.UserInfoClientInfoId,
                        principalTable: "ClientInfo",
                        principalColumn: "ClientInfoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Rentals_UserInfoClientInfoId",
                table: "Rentals",
                column: "UserInfoClientInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Rentals");

            migrationBuilder.DropTable(
                name: "ClientInfo");
        }
    }
}
