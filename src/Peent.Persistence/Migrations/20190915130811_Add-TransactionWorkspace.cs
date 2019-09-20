using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class AddTransactionWorkspace : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "WorkspaceId",
                table: "Transactions",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<int>(
                name: "ForeignCurrencyId",
                table: "TransactionEntries",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_WorkspaceId",
                table: "Transactions",
                column: "WorkspaceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Workspaces_WorkspaceId",
                table: "Transactions",
                column: "WorkspaceId",
                principalTable: "Workspaces",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Workspaces_WorkspaceId",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_WorkspaceId",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "WorkspaceId",
                table: "Transactions");

            migrationBuilder.AlterColumn<int>(
                name: "ForeignCurrencyId",
                table: "TransactionEntries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);
        }
    }
}
