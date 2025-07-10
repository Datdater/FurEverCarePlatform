using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurEverCarePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeTypeInWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "price",
                table: "Wallets",
                newName: "Price");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Price",
                table: "Wallets",
                newName: "price");
        }
    }
}
