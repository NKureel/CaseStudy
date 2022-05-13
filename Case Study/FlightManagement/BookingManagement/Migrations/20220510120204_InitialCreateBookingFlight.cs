using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingManagement.Migrations
{
    public partial class InitialCreateBookingFlight : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingTbls_person_PeopleId",
                table: "bookingTbls");

            migrationBuilder.DropIndex(
                name: "IX_bookingTbls_PeopleId",
                table: "bookingTbls");

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "flightDetail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "SeatClass",
                table: "flightDetail",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "PeopleId",
                table: "bookingTbls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookingTbls_PeopleId",
                table: "bookingTbls",
                column: "PeopleId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_bookingTbls_person_PeopleId",
                table: "bookingTbls",
                column: "PeopleId",
                principalTable: "person",
                principalColumn: "PeopleId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingTbls_person_PeopleId",
                table: "bookingTbls");

            migrationBuilder.DropIndex(
                name: "IX_bookingTbls_PeopleId",
                table: "bookingTbls");

            migrationBuilder.AlterColumn<int>(
                name: "status",
                table: "flightDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "SeatClass",
                table: "flightDetail",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "PeopleId",
                table: "bookingTbls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_bookingTbls_PeopleId",
                table: "bookingTbls",
                column: "PeopleId",
                unique: true,
                filter: "[PeopleId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingTbls_person_PeopleId",
                table: "bookingTbls",
                column: "PeopleId",
                principalTable: "person",
                principalColumn: "PeopleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
