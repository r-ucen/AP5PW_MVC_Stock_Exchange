using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class SeedPortfolioAndPortfolioStock : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioStock_Portfolio_PortfolioId",
                table: "PortfolioStock");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioStock_PortfolioId",
                table: "PortfolioStock");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Portfolio",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "PortfolioId",
                table: "PortfolioStock",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(255)")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioStock_PortfolioId",
                table: "PortfolioStock",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioStock_Portfolio_PortfolioId",
                table: "PortfolioStock",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.InsertData(
                table: "Portfolio",
                columns: new[] { "Id", "Deposits", "UserId" },
                values: new object[,]
                {
                    { 1, 10000.00m, 1 },
                    { 2, 15000.00m, 2 }
                });

            migrationBuilder.InsertData(
                table: "PortfolioStock",
                columns: new[] { "Id", "AvgPurchasePrice", "PortfolioId", "Quantity", "StockId" },
                values: new object[,]
                {
                    { 1, 170.00m, 1, 10m, 1 },
                    { 2, 395.00m, 1, 8m, 2 },
                    { 3, 275.50m, 2, 5m, 3 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Portfolio",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DropForeignKey(
                name: "FK_PortfolioStock_Portfolio_PortfolioId",
                table: "PortfolioStock");

            migrationBuilder.DropIndex(
                name: "IX_PortfolioStock_PortfolioId",
                table: "PortfolioStock");

            migrationBuilder.AlterColumn<string>(
                name: "Id",
                table: "Portfolio",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AlterColumn<string>(
                name: "PortfolioId",
                table: "PortfolioStock",
                type: "varchar(255)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int")
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_PortfolioStock_PortfolioId",
                table: "PortfolioStock",
                column: "PortfolioId");

            migrationBuilder.AddForeignKey(
                name: "FK_PortfolioStock_Portfolio_PortfolioId",
                table: "PortfolioStock",
                column: "PortfolioId",
                principalTable: "Portfolio",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}