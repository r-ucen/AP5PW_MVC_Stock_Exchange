using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ChangeOrderAndTransactionTypeValueToBUYFromBuy : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderType",
                value: "BUY");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderType",
                value: "BUY");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderType",
                value: "BUY");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionType",
                value: "BUY");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionType",
                value: "BUY");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionType",
                value: "BUY");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "OrderType",
                value: "Buy");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "OrderType",
                value: "Buy");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "OrderType",
                value: "Buy");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "TransactionType",
                value: "Buy");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "TransactionType",
                value: "Buy");

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "TransactionType",
                value: "Buy");
        }
    }
}
