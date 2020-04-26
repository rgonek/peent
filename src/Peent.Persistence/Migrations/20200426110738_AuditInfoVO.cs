using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Peent.Persistence.Migrations
{
    public partial class AuditInfoVO : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_CreatedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_LastModifiedById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_LastModifiedById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_CreatedById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_LastModifiedById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_CreatedById",
                table: "TransactionEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_LastModifiedById",
                table: "TransactionEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_CreatedById",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_LastModifiedById",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_CreatedById",
                table: "Workspaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_LastModifiedById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_CreatedById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_LastModifiedById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CreatedById",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LastModifiedById",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_CreatedById",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_LastModifiedById",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_Tags_CreatedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_LastModifiedById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LastModifiedById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LastModifiedById",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Workspaces",
                newName: "Created_ById");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Workspaces",
                newName: "Created_On");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Workspaces",
                newName: "LastModified_ById");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "Workspaces",
                newName: "LastModified_On");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Transactions",
                newName: "Created_ById");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Transactions",
                newName: "Created_On");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Transactions",
                newName: "LastModified_ById");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "Transactions",
                newName: "LastModified_On");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "TransactionEntries",
                newName: "Created_ById");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "TransactionEntries",
                newName: "Created_On");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "TransactionEntries",
                newName: "LastModified_ById");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "TransactionEntries",
                newName: "LastModified_On");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Tags",
                newName: "Created_ById");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Tags",
                newName: "Created_On");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Tags",
                newName: "LastModified_ById");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "Tags",
                newName: "LastModified_On");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Categories",
                newName: "Created_ById");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Categories",
                newName: "Created_On");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Categories",
                newName: "LastModified_ById");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "Categories",
                newName: "LastModified_On");

            migrationBuilder.RenameColumn(
                name: "CreatedById",
                table: "Accounts",
                newName: "Created_ById");

            migrationBuilder.RenameColumn(
                name: "CreationDate",
                table: "Accounts",
                newName: "Created_On");

            migrationBuilder.RenameColumn(
                name: "LastModifiedById",
                table: "Accounts",
                newName: "LastModified_ById");

            migrationBuilder.RenameColumn(
                name: "LastModificationDate",
                table: "Accounts",
                newName: "LastModified_On");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_Created_ById",
                table: "Workspaces",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_LastModified_ById",
                table: "Workspaces",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_Created_ById",
                table: "Transactions",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LastModified_ById",
                table: "Transactions",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_Created_ById",
                table: "TransactionEntries",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_LastModified_ById",
                table: "TransactionEntries",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_Created_ById",
                table: "Tags",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LastModified_ById",
                table: "Tags",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_Created_ById",
                table: "Categories",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastModified_ById",
                table: "Categories",
                column: "LastModified_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_Created_ById",
                table: "Accounts",
                column: "Created_ById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastModified_ById",
                table: "Accounts",
                column: "LastModified_ById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_Created_ById",
                table: "Accounts",
                column: "Created_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_LastModified_ById",
                table: "Accounts",
                column: "LastModified_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_Created_ById",
                table: "Categories",
                column: "Created_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_LastModified_ById",
                table: "Categories",
                column: "LastModified_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_Created_ById",
                table: "Tags",
                column: "Created_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_LastModified_ById",
                table: "Tags",
                column: "LastModified_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_Created_ById",
                table: "TransactionEntries",
                column: "Created_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_LastModified_ById",
                table: "TransactionEntries",
                column: "LastModified_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_Created_ById",
                table: "Transactions",
                column: "Created_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_LastModified_ById",
                table: "Transactions",
                column: "LastModified_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_Created_ById",
                table: "Workspaces",
                column: "Created_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_LastModified_ById",
                table: "Workspaces",
                column: "LastModified_ById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_Created_ById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_AspNetUsers_LastModified_ById",
                table: "Accounts");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_Created_ById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Categories_AspNetUsers_LastModified_ById",
                table: "Categories");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_Created_ById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_Tags_AspNetUsers_LastModified_ById",
                table: "Tags");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_Created_ById",
                table: "TransactionEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_LastModified_ById",
                table: "TransactionEntries");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_Created_ById",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_AspNetUsers_LastModified_ById",
                table: "Transactions");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_Created_ById",
                table: "Workspaces");

            migrationBuilder.DropForeignKey(
                name: "FK_Workspaces_AspNetUsers_LastModified_ById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_Created_ById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Workspaces_LastModified_ById",
                table: "Workspaces");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_Created_ById",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_LastModified_ById",
                table: "Transactions");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_Created_ById",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_TransactionEntries_LastModified_ById",
                table: "TransactionEntries");

            migrationBuilder.DropIndex(
                name: "IX_Tags_Created_ById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Tags_LastModified_ById",
                table: "Tags");

            migrationBuilder.DropIndex(
                name: "IX_Categories_Created_ById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Categories_LastModified_ById",
                table: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_Created_ById",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LastModified_ById",
                table: "Accounts");

            migrationBuilder.RenameColumn(
                newName: "CreatedById",
                table: "Workspaces",
                name: "Created_ById");

            migrationBuilder.RenameColumn(
                newName: "CreationDate",
                table: "Workspaces",
                name: "Created_On");

            migrationBuilder.RenameColumn(
                newName: "LastModifiedById",
                table: "Workspaces",
                name: "LastModified_ById");

            migrationBuilder.RenameColumn(
                newName: "LastModificationDate",
                table: "Workspaces",
                name: "LastModified_On");

            migrationBuilder.RenameColumn(
                newName: "CreatedById",
                table: "Transactions",
                name: "Created_ById");

            migrationBuilder.RenameColumn(
                newName: "CreationDate",
                table: "Transactions",
                name: "Created_On");

            migrationBuilder.RenameColumn(
                newName: "LastModifiedById",
                table: "Transactions",
                name: "LastModified_ById");

            migrationBuilder.RenameColumn(
                newName: "LastModificationDate",
                table: "Transactions",
                name: "LastModified_On");

            migrationBuilder.RenameColumn(
                newName: "CreatedById",
                table: "TransactionEntries",
                name: "Created_ById");

            migrationBuilder.RenameColumn(
                newName: "CreationDate",
                table: "TransactionEntries",
                name: "Created_On");

            migrationBuilder.RenameColumn(
                newName: "LastModifiedById",
                table: "TransactionEntries",
                name: "LastModified_ById");

            migrationBuilder.RenameColumn(
                newName: "LastModificationDate",
                table: "TransactionEntries",
                name: "LastModified_On");

            migrationBuilder.RenameColumn(
                newName: "CreatedById",
                table: "Tags",
                name: "Created_ById");

            migrationBuilder.RenameColumn(
                newName: "CreationDate",
                table: "Tags",
                name: "Created_On");

            migrationBuilder.RenameColumn(
                newName: "LastModifiedById",
                table: "Tags",
                name: "LastModified_ById");

            migrationBuilder.RenameColumn(
                newName: "LastModificationDate",
                table: "Tags",
                name: "LastModified_On");

            migrationBuilder.RenameColumn(
                newName: "CreatedById",
                table: "Categories",
                name: "Created_ById");

            migrationBuilder.RenameColumn(
                newName: "CreationDate",
                table: "Categories",
                name: "Created_On");

            migrationBuilder.RenameColumn(
                newName: "LastModifiedById",
                table: "Categories",
                name: "LastModified_ById");

            migrationBuilder.RenameColumn(
                newName: "LastModificationDate",
                table: "Categories",
                name: "LastModified_On");

            migrationBuilder.RenameColumn(
                newName: "CreatedById",
                table: "Accounts",
                name: "Created_ById");

            migrationBuilder.RenameColumn(
                newName: "CreationDate",
                table: "Accounts",
                name: "Created_On");

            migrationBuilder.RenameColumn(
                newName: "LastModifiedById",
                table: "Accounts",
                name: "LastModified_ById");

            migrationBuilder.RenameColumn(
                newName: "LastModificationDate",
                table: "Accounts",
                name: "LastModified_On");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_CreatedById",
                table: "Workspaces",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Workspaces_LastModifiedById",
                table: "Workspaces",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CreatedById",
                table: "Transactions",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_LastModifiedById",
                table: "Transactions",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_CreatedById",
                table: "TransactionEntries",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_TransactionEntries_LastModifiedById",
                table: "TransactionEntries",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_CreatedById",
                table: "Tags",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Tags_LastModifiedById",
                table: "Tags",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_CreatedById",
                table: "Categories",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Categories_LastModifiedById",
                table: "Categories",
                column: "LastModifiedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_CreatedById",
                table: "Accounts",
                column: "CreatedById");

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LastModifiedById",
                table: "Accounts",
                column: "LastModifiedById");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_CreatedById",
                table: "Accounts",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_AspNetUsers_LastModifiedById",
                table: "Accounts",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_CreatedById",
                table: "Categories",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Categories_AspNetUsers_LastModifiedById",
                table: "Categories",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_CreatedById",
                table: "Tags",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Tags_AspNetUsers_LastModifiedById",
                table: "Tags",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_CreatedById",
                table: "TransactionEntries",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_TransactionEntries_AspNetUsers_LastModifiedById",
                table: "TransactionEntries",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_CreatedById",
                table: "Transactions",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_AspNetUsers_LastModifiedById",
                table: "Transactions",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_CreatedById",
                table: "Workspaces",
                column: "CreatedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Workspaces_AspNetUsers_LastModifiedById",
                table: "Workspaces",
                column: "LastModifiedById",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
