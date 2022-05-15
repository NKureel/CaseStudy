using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingManagement.Migrations
{
    public partial class InitialCreateBookingDbForUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingTbls_person_PeopleId",
                table: "bookingTbls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_person",
                table: "person");

            migrationBuilder.DropIndex(
                name: "IX_bookingTbls_PeopleId",
                table: "bookingTbls");

            migrationBuilder.DropColumn(
                name: "PeopleId",
                table: "bookingTbls");

            migrationBuilder.RenameTable(
                name: "person",
                newName: "Person");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "Person",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSeatBook",
                table: "bookingTbls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "personId",
                table: "bookingTbls",
                type: "int",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Person",
                table: "Person",
                column: "PeopleId");

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

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_bookingTbls_Person_personId",
                table: "bookingTbls");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Person",
                table: "Person");

            migrationBuilder.DropIndex(
                name: "IX_bookingTbls_personId",
                table: "bookingTbls");

            migrationBuilder.DropColumn(
                name: "personId",
                table: "bookingTbls");

            migrationBuilder.RenameTable(
                name: "Person",
                newName: "person");

            migrationBuilder.AlterColumn<int>(
                name: "Age",
                table: "person",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "NoOfSeatBook",
                table: "bookingTbls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PeopleId",
                table: "bookingTbls",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_person",
                table: "person",
                column: "PeopleId");

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
    }
}
