using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingManagement.Migrations
{
    public partial class InitialCreateDb : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "bookflightTbl",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    EmailId = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Meal = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FlightNumber = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Pnr = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    peopleid = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_bookflightTbl", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "UserDetailTbl",
                columns: table => new
                {
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Age = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Class = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailTbl", x => x.PeopleId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "bookflightTbl");

            migrationBuilder.DropTable(
                name: "UserDetailTbl");
        }
    }
}
