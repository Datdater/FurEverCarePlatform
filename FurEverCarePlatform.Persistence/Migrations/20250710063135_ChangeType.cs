using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FurEverCarePlatform.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChangeType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TransactionId",
                table: "Payments",
                newName: "Code");

            migrationBuilder.Sql(@"
                ALTER TABLE ""Payments"" 
                ALTER COLUMN ""Amount"" TYPE real 
                USING (""Amount""::numeric)::real;
            ");

        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Code",
                table: "Payments",
                newName: "PaymentCode");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "Payments",
                type: "money",
                nullable: false,
                oldClrType: typeof(float),
                oldType: "real");
        }
    }
}
