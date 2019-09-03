using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class AddAccountType : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<short>(
                name: "Type",
                table: "Accounts",
                nullable: false,
                defaultValue: (short)0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Type",
                table: "Accounts");
        }
    }
}
