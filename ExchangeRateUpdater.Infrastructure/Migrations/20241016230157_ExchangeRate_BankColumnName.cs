using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRateUpdater.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ExchangeRate_BankColumnName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Banks_BankId",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "BankId",
                table: "ExchangeRates",
                newName: "Bank");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_BankId",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_Bank");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Banks_Bank",
                table: "ExchangeRates",
                column: "Bank",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ExchangeRates_Banks_Bank",
                table: "ExchangeRates");

            migrationBuilder.RenameColumn(
                name: "Bank",
                table: "ExchangeRates",
                newName: "BankId");

            migrationBuilder.RenameIndex(
                name: "IX_ExchangeRates_Bank",
                table: "ExchangeRates",
                newName: "IX_ExchangeRates_BankId");

            migrationBuilder.AddForeignKey(
                name: "FK_ExchangeRates_Banks_BankId",
                table: "ExchangeRates",
                column: "BankId",
                principalTable: "Banks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
