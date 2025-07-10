using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurEverCarePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddWallet : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "WalletId",
                table: "Stores",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Transactions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<string>(type: "text", nullable: true),
                    PaymentMethod = table.Column<int>(type: "integer", nullable: false),
                    StoreId = table.Column<Guid>(type: "uuid", nullable: true),
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transactions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Transactions_AspNetUsers_AppUserId",
                        column: x => x.AppUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Transactions_Stores_StoreId",
                        column: x => x.StoreId,
                        principalTable: "Stores",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Wallets",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    price = table.Column<float>(type: "real", nullable: false),
                    CreationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    ModificationDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    ModifiedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    DeleteDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    DeletedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wallets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Stores_WalletId",
                table: "Stores",
                column: "WalletId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_AppUserId",
                table: "Transactions",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_StoreId",
                table: "Transactions",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores",
                column: "WalletId",
                principalTable: "Wallets",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Stores_Wallets_WalletId",
                table: "Stores");

            migrationBuilder.DropTable(
                name: "Transactions");

            migrationBuilder.DropTable(
                name: "Wallets");

            migrationBuilder.DropIndex(
                name: "IX_Stores_WalletId",
                table: "Stores");

            migrationBuilder.DropColumn(
                name: "WalletId",
                table: "Stores");
        }
    }
}
