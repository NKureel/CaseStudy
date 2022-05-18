using Microsoft.EntityFrameworkCore.Migrations;

namespace BookingManagement.Migrations
{
    public partial class InitialCreateB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserDetailTbl");

            migrationBuilder.DropColumn(
                name: "personId",
                table: "bookflightTbl");

            migrationBuilder.AddColumn<string>(
                name: "Age",
                table: "bookflightTbl",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Class",
                table: "bookflightTbl",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "FirstName",
                table: "bookflightTbl",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                table: "bookflightTbl",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "LastName",
                table: "bookflightTbl",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "bookflightTbl");

            migrationBuilder.DropColumn(
                name: "Class",
                table: "bookflightTbl");

            migrationBuilder.DropColumn(
                name: "FirstName",
                table: "bookflightTbl");

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "bookflightTbl");

            migrationBuilder.DropColumn(
                name: "LastName",
                table: "bookflightTbl");

            migrationBuilder.AddColumn<int>(
                name: "personId",
                table: "bookflightTbl",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "UserDetailTbl",
                columns: table => new
                {
                    PeopleId = table.Column<int>(type: "int", nullable: false),
                    Age = table.Column<string>(type: "nchar(10)", fixedLength: true, maxLength: 10, nullable: true),
                    Class = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserDetailTbl", x => x.PeopleId);
                });
        }
    }
}
