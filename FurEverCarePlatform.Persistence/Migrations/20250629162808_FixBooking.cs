using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurEverCarePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class FixBooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_PetServices_ServiceId",
                table: "BookingDetails");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "BookingDetails",
                newName: "PetServiceDetailId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDetails_ServiceId",
                table: "BookingDetails",
                newName: "IX_BookingDetails_PetServiceDetailId");

            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Bookings",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_PetServiceDetails_PetServiceDetailId",
                table: "BookingDetails",
                column: "PetServiceDetailId",
                principalTable: "PetServiceDetails",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingDetails_PetServiceDetails_PetServiceDetailId",
                table: "BookingDetails");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "PetServiceDetailId",
                table: "BookingDetails",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_BookingDetails_PetServiceDetailId",
                table: "BookingDetails",
                newName: "IX_BookingDetails_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingDetails_PetServices_ServiceId",
                table: "BookingDetails",
                column: "ServiceId",
                principalTable: "PetServices",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
