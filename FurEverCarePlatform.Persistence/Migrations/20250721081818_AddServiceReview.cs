using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurEverCarePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddServiceReview : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Feedbacks_FeedbackId",
                table: "Bookings");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_AppUserId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_OrderDetails_Feedbacks_FeedbackId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_OrderDetails_FeedbackId",
                table: "OrderDetails");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_FeedbackId",
                table: "Bookings");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "OrderDetails");

            migrationBuilder.DropColumn(
                name: "FeedbackId",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "Attachment",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Detail",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "Feedbacks");

            migrationBuilder.RenameTable(
                name: "Feedbacks",
                newName: "ServiceReviews");

            migrationBuilder.RenameIndex(
                name: "IX_Feedbacks_AppUserId",
                table: "ServiceReviews",
                newName: "IX_ServiceReviews_AppUserId");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notifications",
                type: "uuid",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uuid");

            migrationBuilder.AddColumn<Guid>(
                name: "StoreId",
                table: "Notifications",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "ServiceReviews",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "BookingId",
                table: "ServiceReviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Comment",
                table: "ServiceReviews",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<Guid>(
                name: "PetServiceId",
                table: "ServiceReviews",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ServiceReviews",
                table: "ServiceReviews",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_StoreId",
                table: "Notifications",
                column: "StoreId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReviews_BookingId",
                table: "ServiceReviews",
                column: "BookingId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_ServiceReviews_PetServiceId",
                table: "ServiceReviews",
                column: "PetServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Stores_StoreId",
                table: "Notifications",
                column: "StoreId",
                principalTable: "Stores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_AspNetUsers_AppUserId",
                table: "ServiceReviews",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_Bookings_BookingId",
                table: "ServiceReviews",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ServiceReviews_PetServices_PetServiceId",
                table: "ServiceReviews",
                column: "PetServiceId",
                principalTable: "PetServices",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Stores_StoreId",
                table: "Notifications");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_AspNetUsers_AppUserId",
                table: "ServiceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_Bookings_BookingId",
                table: "ServiceReviews");

            migrationBuilder.DropForeignKey(
                name: "FK_ServiceReviews_PetServices_PetServiceId",
                table: "ServiceReviews");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_StoreId",
                table: "Notifications");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ServiceReviews",
                table: "ServiceReviews");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReviews_BookingId",
                table: "ServiceReviews");

            migrationBuilder.DropIndex(
                name: "IX_ServiceReviews_PetServiceId",
                table: "ServiceReviews");

            migrationBuilder.DropColumn(
                name: "StoreId",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "BookingId",
                table: "ServiceReviews");

            migrationBuilder.DropColumn(
                name: "Comment",
                table: "ServiceReviews");

            migrationBuilder.DropColumn(
                name: "PetServiceId",
                table: "ServiceReviews");

            migrationBuilder.RenameTable(
                name: "ServiceReviews",
                newName: "Feedbacks");

            migrationBuilder.RenameIndex(
                name: "IX_ServiceReviews_AppUserId",
                table: "Feedbacks",
                newName: "IX_Feedbacks_AppUserId");

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackId",
                table: "OrderDetails",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "Notifications",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uuid",
                oldNullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "FeedbackId",
                table: "Bookings",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "Rating",
                table: "Feedbacks",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddColumn<string>(
                name: "Attachment",
                table: "Feedbacks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Detail",
                table: "Feedbacks",
                type: "text",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Status",
                table: "Feedbacks",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Feedbacks",
                table: "Feedbacks",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_FeedbackId",
                table: "OrderDetails",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FeedbackId",
                table: "Bookings",
                column: "FeedbackId",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Feedbacks_FeedbackId",
                table: "Bookings",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_AppUserId",
                table: "Feedbacks",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_AspNetUsers_UserId",
                table: "Notifications",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_OrderDetails_Feedbacks_FeedbackId",
                table: "OrderDetails",
                column: "FeedbackId",
                principalTable: "Feedbacks",
                principalColumn: "Id");
        }
    }
}
