using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class BasketProCount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BaskerID",
                table: "BasketProducts");

            migrationBuilder.RenameColumn(
                name: "BaskerID",
                table: "BasketProducts",
                newName: "BasketID");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BaskerID",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BasketID");

            migrationBuilder.AddColumn<int>(
                name: "ProductCount",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BasketID",
                table: "BasketProducts",
                column: "BasketID",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BasketID",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "ProductCount",
                table: "Baskets");

            migrationBuilder.RenameColumn(
                name: "BasketID",
                table: "BasketProducts",
                newName: "BaskerID");

            migrationBuilder.RenameIndex(
                name: "IX_BasketProducts_BasketID",
                table: "BasketProducts",
                newName: "IX_BasketProducts_BaskerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BaskerID",
                table: "BasketProducts",
                column: "BaskerID",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
