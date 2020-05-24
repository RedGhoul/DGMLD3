using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class addedSVGToGraph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SVG",
                table: "Graphs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SVG",
                table: "Graphs");
        }
    }
}
