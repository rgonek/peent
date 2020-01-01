using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class TransactionNotRequiredForeignCurrency : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "ForeignCurrencyId",
                table: "TransactionEntries",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
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
