using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarahStoreDataAccess.Migrations
{
    public partial class UpdatedProductModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsSelectedProduct",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsSpecialSale",
                table: "Products",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "ShowCount",
                table: "Products",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsSelectedProduct",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "IsSpecialSale",
                table: "Products");

            migrationBuilder.DropColumn(
                name: "ShowCount",
                table: "Products");
        }
    }
}
