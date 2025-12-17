using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketIds : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 1,
                column: "MarketId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 2,
                column: "MarketId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 3,
                column: "MarketId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 4,
                column: "MarketId",
                value: 1);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 5,
                column: "MarketId",
                value: 1);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 1,
                column: "MarketId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 2,
                column: "MarketId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 3,
                column: "MarketId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 4,
                column: "MarketId",
                value: null);

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 5,
                column: "MarketId",
                value: null);
        }
    }
}
