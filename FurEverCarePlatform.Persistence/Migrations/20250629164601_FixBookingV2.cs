using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurEverCarePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixBookingV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "UserId",
                table: "Bookings",
                newName: "StoreId");

            migrationBuilder.AlterColumn<bool>(
                name: "PetType",
                table: "Pets",
                type: "boolean",
                nullable: false,
                defaultValue: false,
                oldClrType: typeof(bool),
                oldType: "boolean",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_StoreId",
                table: "Bookings",
                column: "StoreId");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Stores_StoreId",
                table: "Bookings",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Stores_StoreId",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_StoreId",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "StoreId",
                table: "Bookings",
                newName: "UserId");

            migrationBuilder.AlterColumn<bool>(
                name: "PetType",
                table: "Pets",
                type: "boolean",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "boolean");
        }
    }
}
