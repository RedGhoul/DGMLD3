using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class AddedGraphLinkURL : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "GraphLinkURL",
                table: "Graphs",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "GraphLinkURL",
                table: "Graphs");
        }
    }
}
