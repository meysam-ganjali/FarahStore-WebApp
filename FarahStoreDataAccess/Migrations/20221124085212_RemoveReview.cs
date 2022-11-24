using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FarahStoreDataAccess.Migrations
{
    public partial class RemoveReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductReViews");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "ProductReViews",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductId = table.Column<int>(type: "int", nullable: false),
                    ReviewDescription = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReviewImagePath = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ReviewTitle = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductReViews", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductReViews_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ProductReViews_ProductId",
                table: "ProductReViews",
                column: "ProductId");
        }
    }
}
