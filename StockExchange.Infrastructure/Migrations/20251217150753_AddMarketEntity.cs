using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddMarketEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MarketId",
                table: "Stock",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Market",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    TimeZoneId = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    OpenTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    CloseTime = table.Column<TimeSpan>(type: "time(6)", nullable: false),
                    IsCurrentlyOpen = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Market", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Market",
                columns: new[] { "Id", "CloseTime", "IsCurrentlyOpen", "Name", "OpenTime", "TimeZoneId" },
                values: new object[] { 1, new TimeSpan(0, 16, 0, 0, 0), false, "NYSE", new TimeSpan(0, 9, 30, 0, 0), "Eastern Standard Time" });

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

            migrationBuilder.CreateIndex(
                name: "IX_Stock_MarketId",
                table: "Stock",
                column: "MarketId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stock_Market_MarketId",
                table: "Stock",
                column: "MarketId",
                principalTable: "Market",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stock_Market_MarketId",
                table: "Stock");

            migrationBuilder.DropTable(
                name: "Market");

            migrationBuilder.DropIndex(
                name: "IX_Stock_MarketId",
                table: "Stock");

            migrationBuilder.DropColumn(
                name: "MarketId",
                table: "Stock");
        }
    }
}
