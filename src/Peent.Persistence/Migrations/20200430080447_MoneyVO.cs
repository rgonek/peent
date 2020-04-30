using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class MoneyVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "TransactionEntries",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "TransactionEntries",
                type: "decimal(38,18)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,18)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "CurrencyId",
                table: "TransactionEntries",
                type: "int",
                nullable: false,
                oldClrType: typeof(int),
                oldNullable: true);

            migrationBuilder.AlterColumn<decimal>(
                name: "Amount",
                table: "TransactionEntries",
                type: "decimal(38,18)",
                nullable: false,
                oldClrType: typeof(decimal),
                oldType: "decimal(38,18)",
                oldNullable: true);
        }
    }
}
