using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Shop.DataAccess.Migrations
{
    public partial class CreatedRelations : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInvoices",
                table: "ProductInvoices");

            migrationBuilder.DropIndex(
                name: "IX_ProductInvoices_ProductId",
                table: "ProductInvoices");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_BasketId",
                table: "BasketProducts");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_ProductId",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ProductInvoices");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "BasketId",
                table: "BasketProducts");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Invoices",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                table: "Baskets",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInvoices",
                table: "ProductInvoices",
                columns: new[] { "ProductId", "InvoiceId" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                columns: new[] { "ProductId", "BaskerID" });

            migrationBuilder.CreateIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_BaskerID",
                table: "BasketProducts",
                column: "BaskerID");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BaskerID",
                table: "BasketProducts",
                column: "BaskerID",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoices_Users_UserId",
                table: "Invoices",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BasketProducts_Baskets_BaskerID",
                table: "BasketProducts");

            migrationBuilder.DropForeignKey(
                name: "FK_Baskets_Users_UserId",
                table: "Baskets");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoices_Users_UserId",
                table: "Invoices");

            migrationBuilder.DropForeignKey(
                name: "FK_Wallets_Users_UserId",
                table: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Wallets_UserId",
                table: "Wallets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ProductInvoices",
                table: "ProductInvoices");

            migrationBuilder.DropIndex(
                name: "IX_Invoices_UserId",
                table: "Invoices");

            migrationBuilder.DropIndex(
                name: "IX_Baskets_UserId",
                table: "Baskets");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts");

            migrationBuilder.DropIndex(
                name: "IX_BasketProducts_BaskerID",
                table: "BasketProducts");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Baskets");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ProductInvoices",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "BasketProducts",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddColumn<int>(
                name: "BasketId",
                table: "BasketProducts",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ProductInvoices",
                table: "ProductInvoices",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BasketProducts",
                table: "BasketProducts",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoices_ProductId",
                table: "ProductInvoices",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_BasketId",
                table: "BasketProducts",
                column: "BasketId");

            migrationBuilder.CreateIndex(
                name: "IX_BasketProducts_ProductId",
                table: "BasketProducts",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_BasketProducts_Baskets_BasketId",
                table: "BasketProducts",
                column: "BasketId",
                principalTable: "Baskets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
