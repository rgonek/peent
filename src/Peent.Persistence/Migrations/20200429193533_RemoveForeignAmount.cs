using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class RemoveForeignAmount : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_Currencies_ForeignCurrencyId",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_ForeignCurrencyId",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "ForeignAmount",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "ForeignCurrencyId",
                table: "TransactionEntries");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<decimal>(
                name: "ForeignAmount",
                table: "TransactionEntries",
                type: "decimal(38,18)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ForeignCurrencyId",
                table: "TransactionEntries",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_ForeignCurrencyId",
                table: "TransactionEntries",
                column: "ForeignCurrencyId");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_Currencies_ForeignCurrencyId",
                table: "TransactionEntries",
                column: "ForeignCurrencyId",
                principalTable: "Currencies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
