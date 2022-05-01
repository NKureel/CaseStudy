using Microsoft.EntityFrameworkCore.Migrations;

namespace Booking.Migrations
{
    public partial class InitialBookingAdd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Gender = table.Column<int>(type: "int", nullable: false),
                    age = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "bookingTbls",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    NoOfSeatBook = table.Column<int>(type: "int", nullable: false),
                    Peopleid = table.Column<int>(type: "int", nullable: true),
                    Meal = table.Column<int>(type: "int", nullable: false),
                    SeatNo = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FlightNumber = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookingTbls", x => x.id);
                    table.ForeignKey(
                        name: "FK_bookingTbls_Person_Peopleid",
                        column: x => x.Peopleid,
                        principalTable: "Person",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_bookingTbls_Peopleid",
                table: "bookingTbls",
                column: "Peopleid");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookingTbls");

            migrationBuilder.DropTable(
                name: "Person");
        }
    }
}
