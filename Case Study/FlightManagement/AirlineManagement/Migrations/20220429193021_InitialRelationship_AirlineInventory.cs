using Microsoft.EntityFrameworkCore.Migrations;

namespace AirlineManagement.Migrations
{
    public partial class InitialRelationship_AirlineInventory : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryTbls_airlineTbls_AirlineNo",
                table: "inventoryTbls");

            migrationBuilder.AlterColumn<string>(
                name: "AirlineNo",
                table: "inventoryTbls",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryTbls_airlineTbls_AirlineNo",
                table: "inventoryTbls",
                column: "AirlineNo",
                principalTable: "airlineTbls",
                principalColumn: "AirlineNo",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_inventoryTbls_airlineTbls_AirlineNo",
                table: "inventoryTbls");

            migrationBuilder.AlterColumn<string>(
                name: "AirlineNo",
                table: "inventoryTbls",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_inventoryTbls_airlineTbls_AirlineNo",
                table: "inventoryTbls",
                column: "AirlineNo",
                principalTable: "airlineTbls",
                principalColumn: "AirlineNo",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
