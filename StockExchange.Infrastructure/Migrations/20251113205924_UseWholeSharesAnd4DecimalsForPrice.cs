using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UseWholeSharesAnd4DecimalsForPrice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Transaction",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Transaction",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Stock",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "PortfolioStock",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvgPurchasePrice",
                table: "PortfolioStock",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,2)");

            migrationBuilder.AlterColumn<int>(
                name: "Quantity",
                table: "Order",
                type: "int",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 8);

            migrationBuilder.UpdateData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 10);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 5);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 8);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Transaction",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Price",
                table: "Transaction",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "CurrentPrice",
                table: "Stock",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "PortfolioStock",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "AvgPurchasePrice",
                table: "PortfolioStock",
                type: "decimal(18,2)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(18,4)");

            migrationBuilder.AlterColumn<decimal>(
                name: "Quantity",
                table: "Order",
                type: "decimal(18,4)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 5m);

            migrationBuilder.UpdateData(
                table: "Order",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 8m);

            migrationBuilder.UpdateData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 8m);

            migrationBuilder.UpdateData(
                table: "PortfolioStock",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 5m);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 1,
                column: "Quantity",
                value: 10m);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 2,
                column: "Quantity",
                value: 5m);

            migrationBuilder.UpdateData(
                table: "Transaction",
                keyColumn: "Id",
                keyValue: 3,
                column: "Quantity",
                value: 8m);
        }
    }
}
