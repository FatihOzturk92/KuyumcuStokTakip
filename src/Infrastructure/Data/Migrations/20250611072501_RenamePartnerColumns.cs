using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace KuyumcuStokTakip.Infrastructure.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamePartnerColumns : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ParnerPhone",
                table: "Partners",
                newName: "PartnerPhone");

            migrationBuilder.RenameColumn(
                name: "ParnerEmail",
                table: "Partners",
                newName: "PartnerEmail");

            migrationBuilder.RenameColumn(
                name: "ParnerAddress",
                table: "Partners",
                newName: "PartnerAddress");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "PartnerPhone",
                table: "Partners",
                newName: "ParnerPhone");

            migrationBuilder.RenameColumn(
                name: "PartnerEmail",
                table: "Partners",
                newName: "ParnerEmail");

            migrationBuilder.RenameColumn(
                name: "PartnerAddress",
                table: "Partners",
                newName: "ParnerAddress");
        }
    }
}
