using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuStokTakip.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddWastageReason : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "WastageReason",
                table: "StockTransactions",
                type: "nvarchar(200)",
                maxLength: 200,
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WastageReason",
                table: "StockTransactions");
        }
    }
}
