using Microsoft.EntityFrameworkCore.Migrations;

namespace DGMLD3.Migrations
{
    public partial class ChangedPriceModelAlot : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "active",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "planName",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "price",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Prices",
                newName: "Id");

            migrationBuilder.AddColumn<string>(
                name: "BillingPer",
                table: "Prices",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "ChargeAmount",
                table: "Prices",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<bool>(
                name: "IsActive",
                table: "Prices",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Prices",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BillingPer",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "ChargeAmount",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "IsActive",
                table: "Prices");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Prices");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "Prices",
                newName: "id");

            migrationBuilder.AddColumn<bool>(
                name: "active",
                table: "Prices",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "planName",
                table: "Prices",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "Prices",
                type: "double precision",
                nullable: false,
                defaultValue: 0.0);
        }
    }
}
