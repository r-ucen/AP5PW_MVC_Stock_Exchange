using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddIdentityWithHardcodedDateInStockSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 3,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 4,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 5,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 1,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 101, DateTimeKind.Utc).AddTicks(3199));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 2,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 101, DateTimeKind.Utc).AddTicks(3880));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 3,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 101, DateTimeKind.Utc).AddTicks(3902));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 4,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 101, DateTimeKind.Utc).AddTicks(3905));

            migrationBuilder.UpdateData(
                table: "Stock",
                keyColumn: "Id",
                keyValue: 5,
                column: "CurrentPriceDateTime",
                value: new DateTime(2025, 11, 2, 22, 58, 40, 101, DateTimeKind.Utc).AddTicks(3909));
        }
    }
}
