using Microsoft.EntityFrameworkCore.Migrations;

namespace TheTop.Application.Migrations
{
    public partial class v8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Price",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<int>(
                name: "AdvertisementId",
                table: "ShoppingCarts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_ShoppingCarts_AdvertisementId",
                table: "ShoppingCarts",
                column: "AdvertisementId");

            migrationBuilder.AddForeignKey(
                name: "FK_ShoppingCarts_Advertisements_AdvertisementId",
                table: "ShoppingCarts",
                column: "AdvertisementId",
                principalTable: "Advertisements",
                principalColumn: "AdvertisementId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ShoppingCarts_Advertisements_AdvertisementId",
                table: "ShoppingCarts");

            migrationBuilder.DropIndex(
                name: "IX_ShoppingCarts_AdvertisementId",
                table: "ShoppingCarts");

            migrationBuilder.DropColumn(
                name: "AdvertisementId",
                table: "ShoppingCarts");

            migrationBuilder.AddColumn<decimal>(
                name: "Price",
                table: "ShoppingCarts",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }
    }
}
