using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MoneyMeAPI.Data.Migrations
{
    public partial class AddUniqueConstraints : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Repayments_AccountId",
                table: "Repayments");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AccountId",
                table: "Loans");

            migrationBuilder.CreateIndex(
                name: "IX_Repayments_AccountId",
                table: "Repayments",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                table: "Loans",
                column: "AccountId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Accounts_FirstName_LastName_DateOfBirth",
                table: "Accounts",
                columns: new[] { "FirstName", "LastName", "DateOfBirth" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Repayments_AccountId",
                table: "Repayments");

            migrationBuilder.DropIndex(
                name: "IX_Loans_AccountId",
                table: "Loans");

            migrationBuilder.DropIndex(
                name: "IX_Accounts_FirstName_LastName_DateOfBirth",
                table: "Accounts");

            migrationBuilder.CreateIndex(
                name: "IX_Repayments_AccountId",
                table: "Repayments",
                column: "AccountId");

            migrationBuilder.CreateIndex(
                name: "IX_Loans_AccountId",
                table: "Loans",
                column: "AccountId");
        }
    }
}
