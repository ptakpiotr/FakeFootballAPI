using Microsoft.EntityFrameworkCore.Migrations;

namespace FakeFootball.Migrations.JwtUserDb
{
    public partial class FixJwt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "JwtUsers");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "JwtUsers",
                type: "text",
                nullable: true);
        }
    }
}
