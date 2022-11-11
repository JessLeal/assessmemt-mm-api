using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMeAPI.Data.Migrations
{
    public partial class AddRepaymentsModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Accounts_Loans_LoanId",
                table: "Accounts");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_LoanId",
                table: "Accounts");

            migrationBuilder.DropColumn(
                name: "LoanId",
                table: "Accounts");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Loans",
                type: "TEXT",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<string>(
                name: "Product",
                table: "Loans",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Repayments",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    EstablishmentFee = table.Column<decimal>(type: "TEXT", nullable: false),
                    Interest = table.Column<decimal>(type: "TEXT", nullable: false),
                    AccountId = table.Column<Guid>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Repayments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Repayments_Accounts_AccountId",
                        column: x => x.AccountId,
                        principalTable: "Accounts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                table: "Loans",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Repayments_AccountId",
                table: "Repayments",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Loans_Accounts_AccountId",
                table: "Loans",
                column: "AccountId",
                principalTable: "Accounts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Loans_Accounts_AccountId",
                table: "Loans");

            migrationBuilder.DropTable(
                name: "Repayments");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AccountId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Loans");

            migrationBuilder.DropColumn(
                name: "Product",
                table: "Loans");

            migrationBuilder.AddColumn<Guid>(
                name: "LoanId",
                table: "Accounts",
                type: "TEXT",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_LoanId",
                table: "Accounts",
                column: "LoanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Accounts_Loans_LoanId",
                table: "Accounts",
                column: "LoanId",
                principalTable: "Loans",
                principalColumn: "Id");
        }
    }
}
