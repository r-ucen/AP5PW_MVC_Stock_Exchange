using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCurrentPriceDate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CurrentPriceDateTime",
                table: "Stock");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "CurrentPriceDateTime",
                table: "Stock",
                type: "datetime(6)",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

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
    }
}
