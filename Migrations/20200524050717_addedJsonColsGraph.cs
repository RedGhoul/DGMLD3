using System.Collections.Generic;
using DGMLD3.Data.RDMS;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class addedJsonColsGraph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<ICollection<Link>>(
                name: "Links",
                table: "Graphs",
                type: "jsonb",
                nullable: true);

            migrationBuilder.AddColumn<ICollection<Node>>(
                name: "Nodes",
                table: "Graphs",
                type: "jsonb",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Links",
                table: "Graphs");

            migrationBuilder.DropColumn(
                name: "Nodes",
                table: "Graphs");
        }
    }
}
