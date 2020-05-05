using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class addedIsPublicToGraph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPublic",
                table: "Graphs",
                nullable: false,
                defaultValue: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPublic",
                table: "Graphs");
        }
    }
}
