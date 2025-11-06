using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace StockExchange.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class RemoveNotMapped : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Portfolio_UserId",
                table: "Portfolio",
                column: "UserId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Order_UserId",
                table: "Order",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Portfolio_AspNetUsers_UserId",
                table: "Portfolio",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Order_AspNetUsers_UserId",
                table: "Order");

            migrationBuilder.DropForeignKey(
                name: "FK_Portfolio_AspNetUsers_UserId",
                table: "Portfolio");

            migrationBuilder.DropForeignKey(
                name: "FK_Transaction_AspNetUsers_UserId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Transaction_UserId",
                table: "Transaction");

            migrationBuilder.DropIndex(
                name: "IX_Portfolio_UserId",
                table: "Portfolio");

            migrationBuilder.DropIndex(
                name: "IX_Order_UserId",
                table: "Order");
        }
    }
}
