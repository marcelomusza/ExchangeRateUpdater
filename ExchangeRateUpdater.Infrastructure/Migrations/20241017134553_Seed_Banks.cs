using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExchangeRateUpdater.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Seed_Banks : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Banks",
                columns: new[] { "Id", "Name" },
                values: new object[] { 1, "Czech National Bank" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Banks",
                keyColumn: "Id",
                keyValue: 1);
        }
    }
}
