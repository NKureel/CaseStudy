using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingManagement.Migrations
{
    public partial class InitialCreateBookingDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingTbls_Person_personId",
                table: "bookingTbls");

            migrationBuilder.DropIndex(
                name: "IX_bookingTbls_personId",
                table: "bookingTbls");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Person",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "UserBookingTblid",
                table: "Person",
                type: "int",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "flightDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SeatClass",
                table: "flightDetail",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "SeatClass",
                table: "bookingTbls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Meal",
                table: "bookingTbls",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Person_UserBookingTblid",
                table: "Person",
                column: "UserBookingTblid");

            migrationBuilder.AddForeignKey(
                name: "FK_Person_bookingTbls_UserBookingTblid",
                table: "Person",
                column: "UserBookingTblid",
                principalTable: "bookingTbls",
                principalColumn: "id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Person_bookingTbls_UserBookingTblid",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_Person_UserBookingTblid",
                table: "Person");

            migrationBuilder.DropColumn(
                name: "UserBookingTblid",
                table: "Person");

            migrationBuilder.AlterColumn<string>(
                name: "Gender",
                table: "Person",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "status",
                table: "flightDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SeatClass",
                table: "flightDetail",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "SeatClass",
                table: "bookingTbls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Meal",
                table: "bookingTbls",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_bookingTbls_personId",
                table: "bookingTbls",
                column: "personId");

            migrationBuilder.AddForeignKey(
                name: "FK_bookingTbls_Person_personId",
                table: "bookingTbls",
                column: "personId",
                principalTable: "Person",
                principalColumn: "PeopleId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
