using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class RemoveForeignIds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_Transactions_TransactionId",
                table: "TransactionEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_Transactions_TransactionId1",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_TransactionId1",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "TransactionId1",
                table: "TransactionEntries");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_Transactions_TransactionId",
                table: "TransactionEntries",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.NoAction);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_Transactions_TransactionId",
                table: "TransactionEntries");

            migrationBuilder.AddColumn<long>(
                name: "TransactionId1",
                table: "TransactionEntries",
                type: "bigint",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_TransactionId1",
                table: "TransactionEntries",
                column: "TransactionId1");

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_Transactions_TransactionId",
                table: "TransactionEntries",
                column: "TransactionId",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_Transactions_TransactionId1",
                table: "TransactionEntries",
                column: "TransactionId1",
                principalTable: "Transactions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
