using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DGMLD3.Migrations
{
    public partial class addedPrices : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Link_Graphs_GraphId",
                table: "Link");

            migrationBuilder.DropForeignKey(
                name: "FK_Node_Graphs_GraphId",
                table: "Node");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Node",
                table: "Node");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Link",
                table: "Link");

            migrationBuilder.RenameTable(
                name: "Node",
                newName: "Nodes");

            migrationBuilder.RenameTable(
                name: "Link",
                newName: "Links");

            migrationBuilder.RenameIndex(
                name: "IX_Node_GraphId",
                table: "Nodes",
                newName: "IX_Nodes_GraphId");

            migrationBuilder.RenameIndex(
                name: "IX_Link_GraphId",
                table: "Links",
                newName: "IX_Links_GraphId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Nodes",
                table: "Nodes",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Links",
                table: "Links",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "Prices",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.SerialColumn),
                    planName = table.Column<string>(nullable: true),
                    price = table.Column<double>(nullable: false),
                    active = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Prices", x => x.id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Links_Graphs_GraphId",
                table: "Links",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Nodes_Graphs_GraphId",
                table: "Nodes",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Links_Graphs_GraphId",
                table: "Links");

            migrationBuilder.DropForeignKey(
                name: "FK_Nodes_Graphs_GraphId",
                table: "Nodes");

            migrationBuilder.DropTable(
                name: "Prices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Nodes",
                table: "Nodes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Links",
                table: "Links");

            migrationBuilder.RenameTable(
                name: "Nodes",
                newName: "Node");

            migrationBuilder.RenameTable(
                name: "Links",
                newName: "Link");

            migrationBuilder.RenameIndex(
                name: "IX_Nodes_GraphId",
                table: "Node",
                newName: "IX_Node_GraphId");

            migrationBuilder.RenameIndex(
                name: "IX_Links_GraphId",
                table: "Link",
                newName: "IX_Link_GraphId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Node",
                table: "Node",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Link",
                table: "Link",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Link_Graphs_GraphId",
                table: "Link",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Node_Graphs_GraphId",
                table: "Node",
                column: "GraphId",
                principalTable: "Graphs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
