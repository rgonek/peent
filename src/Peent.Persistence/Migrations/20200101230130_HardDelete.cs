using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class HardDelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_DeletedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_DeletedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_DeletedById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_DeletedById",
                table: "TransactionEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_DeletedById",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_DeletedById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_DeletedById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_DeletedById",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_DeletedById",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_Tags_DeletedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_DeletedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Workspaces");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Transactions");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "TransactionEntries");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Tags");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Categories");

            migrationBuilder.DropColumn(
                name: "DeletedById",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "DeletionDate",
                table: "Accounts");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Workspaces",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Workspaces",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Transactions",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Transactions",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "TransactionEntries",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "TransactionEntries",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Tags",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Tags",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Categories",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Categories",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DeletedById",
                table: "Accounts",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "DeletionDate",
                table: "Accounts",
                type: "datetime2",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_DeletedById",
                table: "Workspaces",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_DeletedById",
                table: "Transactions",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_DeletedById",
                table: "TransactionEntries",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_DeletedById",
                table: "Tags",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_DeletedById",
                table: "Categories",
                column: "DeletedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_DeletedById",
                table: "Accounts",
                column: "DeletedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_DeletedById",
                table: "Accounts",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_DeletedById",
                table: "Categories",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_DeletedById",
                table: "Tags",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_DeletedById",
                table: "TransactionEntries",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_DeletedById",
                table: "Transactions",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_DeletedById",
                table: "Workspaces",
                column: "DeletedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
