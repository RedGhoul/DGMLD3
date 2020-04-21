using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class addedReadableName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ReadableName",
                table: "Graphs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ReadableName",
                table: "Graphs");
        }
    }
}
