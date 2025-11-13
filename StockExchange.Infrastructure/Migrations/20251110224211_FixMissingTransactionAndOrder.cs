using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class FixMissingTransactionAndOrder : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CreatedDate", "OrderType", "Quantity", "StockId", "UserId" },
                values: new object[] { 3, new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified), "Buy", 8m, 2, 1 });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "OrderId", "Price", "Quantity", "StockId", "TransactionDate", "TransactionType", "UserId" },
                values: new object[] { 3, 3, 395.00m, 8m, 2, new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified), "Buy", 1 });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
