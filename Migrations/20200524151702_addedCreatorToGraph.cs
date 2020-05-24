using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class addedCreatorToGraph : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Graphs_AspNetUsers_ApplicationUserId",
                table: "Graphs");

            migrationBuilder.DropIndex(
                name: "IX_Graphs_ApplicationUserId",
                table: "Graphs");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Graphs");

            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Graphs",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Graphs_CreatorId",
                table: "Graphs",
                column: "CreatorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Graphs_AspNetUsers_CreatorId",
                table: "Graphs",
                column: "CreatorId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Graphs_AspNetUsers_CreatorId",
                table: "Graphs");

            migrationBuilder.DropIndex(
                name: "IX_Graphs_CreatorId",
                table: "Graphs");

            migrationBuilder.DropColumn(
                name: "CreatorId",
                table: "Graphs");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Graphs",
                type: "text",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Graphs_ApplicationUserId",
                table: "Graphs",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Graphs_AspNetUsers_ApplicationUserId",
                table: "Graphs",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
