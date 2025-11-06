using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedTransactionsAndOrders : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Order",
                columns: new[] { "Id", "CreatedDate", "OrderType", "Quantity", "StockId", "UserId" },
                values: new object[,]
                {
                    { 1, new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified), "Buy", 10m, 1, 1 },
                    { 2, new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified), "Buy", 5m, 3, 2 }
                });

            migrationBuilder.InsertData(
                table: "Transaction",
                columns: new[] { "Id", "OrderId", "Price", "Quantity", "StockId", "TransactionDate", "TransactionType", "UserId" },
                values: new object[,]
                {
                    { 1, 1, 170.00m, 10m, 1, new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified), "Buy", 1 },
                    { 2, 2, 275.50m, 5m, 3, new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified), "Buy", 2 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2);
        }
    }
}
