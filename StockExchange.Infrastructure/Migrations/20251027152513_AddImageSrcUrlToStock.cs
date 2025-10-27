using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddImageSrcUrlToStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Stocks_StockId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioStocks_Portfolios_PortfolioId",
                table: "PortfolioStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioStocks_Stocks_StockId",
                table: "PortfolioStocks");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Order_OrderId",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Stocks_StockId",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioStocks",
                table: "PortfolioStocks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios");

            migrationBuilder.RenameTable(
                name: "Transactions",
                newName: "Transaction");

            migrationBuilder.RenameTable(
                name: "Stocks",
                newName: "Stock");

            migrationBuilder.RenameTable(
                name: "PortfolioStocks",
                newName: "PortfolioStock");

            migrationBuilder.RenameTable(
                name: "Portfolios",
                newName: "Portfolio");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_StockId",
                table: "Transaction",
                newName: "IX_Transaction_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Transactions_OrderId",
                table: "Transaction",
                newName: "IX_Transaction_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioStocks_StockId",
                table: "PortfolioStock",
                newName: "IX_PortfolioStock_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioStocks_PortfolioId",
                table: "PortfolioStock",
                newName: "IX_PortfolioStock_PortfolioId");

            migrationBuilder.AddColumn<string>(
                name: "ImageSrcUrl",
                table: "Stock",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stock",
                table: "Stock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioStock",
                table: "PortfolioStock",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Stock_StockId",
                table: "Order",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioStock_Portfolio_PortfolioId",
                table: "PortfolioStock",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioStock_Stock_StockId",
                table: "PortfolioStock",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_Stock_StockId",
                table: "Transaction",
                column: "StockId",
                principalTable: "Stock",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_Stock_StockId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioStock_Portfolio_PortfolioId",
                table: "PortfolioStock");

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioStock_Stock_StockId",
                table: "PortfolioStock");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Order_OrderId",
                table: "Transaction");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_Stock_StockId",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Transaction",
                table: "Transaction");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Stock",
                table: "Stock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PortfolioStock",
                table: "PortfolioStock");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Portfolio",
                table: "Portfolio");

            migrationBuilder.DropColumn(
                name: "ImageSrcUrl",
                table: "Stock");

            migrationBuilder.RenameTable(
                name: "Transaction",
                newName: "Transactions");

            migrationBuilder.RenameTable(
                name: "Stock",
                newName: "Stocks");

            migrationBuilder.RenameTable(
                name: "PortfolioStock",
                newName: "PortfolioStocks");

            migrationBuilder.RenameTable(
                name: "Portfolio",
                newName: "Portfolios");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_StockId",
                table: "Transactions",
                newName: "IX_Transactions_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_Transaction_OrderId",
                table: "Transactions",
                newName: "IX_Transactions_OrderId");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioStock_StockId",
                table: "PortfolioStocks",
                newName: "IX_PortfolioStocks_StockId");

            migrationBuilder.RenameIndex(
                name: "IX_PortfolioStock_PortfolioId",
                table: "PortfolioStocks",
                newName: "IX_PortfolioStocks_PortfolioId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Transactions",
                table: "Transactions",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Stocks",
                table: "Stocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PortfolioStocks",
                table: "PortfolioStocks",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Portfolios",
                table: "Portfolios",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_Stocks_StockId",
                table: "Order",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioStocks_Portfolios_PortfolioId",
                table: "PortfolioStocks",
                column: "PortfolioId",
                principalTable: "Portfolios",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioStocks_Stocks_StockId",
                table: "PortfolioStocks",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Order_OrderId",
                table: "Transactions",
                column: "OrderId",
                principalTable: "Order",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Stocks_StockId",
                table: "Transactions",
                column: "StockId",
                principalTable: "Stocks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
