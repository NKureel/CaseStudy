using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineManagement.Migrations
{
    public partial class initialAlterInventoryTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Airline",
                table: "inventoryTbls");

            migrationBuilder.AddColumn<string>(
                name: "AirlineNo",
                table: "inventoryTbls",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_inventoryTbls_AirlineNo",
                table: "inventoryTbls",
                column: "AirlineNo");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryTbls_airlineTbls_AirlineNo",
                table: "inventoryTbls",
                column: "AirlineNo",
                principalTable: "airlineTbls",
                principalColumn: "AirlineNo",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryTbls_airlineTbls_AirlineNo",
                table: "inventoryTbls");

            migrationBuilder.DropIndex(
                name: "IX_inventoryTbls_AirlineNo",
                table: "inventoryTbls");

            migrationBuilder.DropColumn(
                name: "AirlineNo",
                table: "inventoryTbls");

            migrationBuilder.AddColumn<string>(
                name: "Airline",
                table: "inventoryTbls",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
