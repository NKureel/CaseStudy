using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Migrations
{
    public partial class InitialBookingUpdate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Pnr",
                table: "bookingTbls",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Pnr",
                table: "bookingTbls");
        }
    }
}
