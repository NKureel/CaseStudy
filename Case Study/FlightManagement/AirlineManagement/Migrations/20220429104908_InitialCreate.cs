using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineManagement.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "airlineTbls",
                columns: table => new
                {
                    AirlineNo = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    UploadLogo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ContactAddress = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_airlineTbls", x => x.AirlineNo);
                });

            migrationBuilder.CreateTable(
                name: "inventoryTbls",
                columns: table => new
                {
                    FlightNumber = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Airline = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FromPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ToPlace = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    StartDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EndDateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ScheduleDays = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    InstrumentUsed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BusinessClassSeat = table.Column<int>(type: "int", nullable: false),
                    NonBusinessClassSeat = table.Column<int>(type: "int", nullable: false),
                    TicketCost = table.Column<decimal>(type: "decimal(8,2)", nullable: false),
                    NoOfRows = table.Column<int>(type: "int", nullable: false),
                    Meal = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_inventoryTbls", x => x.FlightNumber);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "airlineTbls");

            migrationBuilder.DropTable(
                name: "inventoryTbls");
        }
    }
}
